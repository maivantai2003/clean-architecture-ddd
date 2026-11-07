using Ai.Domain.Common;
using Ai.Domain.Documents.Events;


namespace Ai.Domain.Documents
{
    public sealed class Document : AggregateRoot
    {
        public Guid KnowledgeBaseId { get; private set; }

        public Guid UploadedBy { get; private set; }

        public string FileName { get; private set; } = string.Empty;

        public string ContentType { get; private set; } = string.Empty;

        public long FileSizeBytes { get; private set; }

        // Tham chiếu tới file lưu trữ, không phải nội dung file.
        public string StorageKey { get; private set; } = string.Empty;

        public DocumentStatus Status { get; private set; }

        public int RetryCount { get; private set; }

        public string? FailureCode { get; private set; }

        public DateTimeOffset UploadedAt { get; private set; }

        public DateTimeOffset UpdatedAt { get; private set; }

        public DateTimeOffset? IndexedAt { get; private set; }

        private Document()
        {
        }

        private Document(
            Guid id,
            Guid knowledgeBaseId,
            Guid uploadedBy,
            string fileName,
            string contentType,
            long fileSizeBytes,
            string storageKey,
            DateTimeOffset now) : base(id)
        {
            if (knowledgeBaseId == Guid.Empty)
                throw new DomainException(
                    "KnowledgeBaseId không hợp lệ.");

            if (uploadedBy == Guid.Empty)
                throw new DomainException(
                    "UploadedBy không hợp lệ.");

            if (fileSizeBytes <= 0)
                throw new DomainException(
                    "Kích thước tài liệu phải lớn hơn 0.");

            KnowledgeBaseId = knowledgeBaseId;
            UploadedBy = uploadedBy;
            FileName = NormalizeRequired(fileName, 255, "FileName");
            ContentType = NormalizeRequired(contentType, 150, "ContentType");
            StorageKey = NormalizeRequired(storageKey, 1_000, "StorageKey");

            FileSizeBytes = fileSizeBytes;
            Status = DocumentStatus.Pending;
            RetryCount = 0;
            UploadedAt = now;
            UpdatedAt = now;

            AddDomainEvent(
                new DocumentUploadedDomainEvent(Id, KnowledgeBaseId, now));
        }

        public static Document Create(
            Guid knowledgeBaseId,
            Guid uploadedBy,
            string fileName,
            string contentType,
            long fileSizeBytes,
            string storageKey)
        {
            return new Document(
                Guid.NewGuid(),
                knowledgeBaseId,
                uploadedBy,
                fileName,
                contentType,
                fileSizeBytes,
                storageKey,
                DateTimeOffset.UtcNow);
        }

        public void StartProcessing()
        {
            if (Status is not (
                DocumentStatus.Pending or DocumentStatus.Failed))
            {
                throw new DomainException(
                    "Tài liệu không thể chuyển sang trạng thái xử lý.");
            }

            Status = DocumentStatus.Processing;
            FailureCode = null;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void MarkIndexed()
        {
            if (Status != DocumentStatus.Processing)
                throw new DomainException(
                    "Chỉ tài liệu đang xử lý mới được đánh dấu Indexed.");

            var now = DateTimeOffset.UtcNow;

            Status = DocumentStatus.Indexed;
            IndexedAt = now;
            UpdatedAt = now;
            FailureCode = null;

            AddDomainEvent(
                new DocumentIndexedDomainEvent(Id, KnowledgeBaseId, now));
        }

        public void MarkFailed(string failureCode)
        {
            if (Status != DocumentStatus.Processing)
                throw new DomainException(
                    "Chỉ tài liệu đang xử lý mới có thể thất bại.");

            FailureCode = NormalizeRequired(
                failureCode, 100, "FailureCode");

            Status = DocumentStatus.Failed;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void RecordRetry()
        {
            if (Status != DocumentStatus.Failed)
                throw new DomainException(
                    "Chỉ tài liệu thất bại mới được ghi nhận retry.");

            RetryCount++;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void Delete()
        {
            if (Status == DocumentStatus.Deleted)
                return;

            Status = DocumentStatus.Deleted;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        private static string NormalizeRequired(
            string value,
            int maxLength,
            string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException(
                    $"{fieldName} không được để trống.");

            value = value.Trim();

            if (value.Length > maxLength)
                throw new DomainException(
                    $"{fieldName} vượt quá độ dài cho phép.");

            return value;
        }
    }
}
