using Ai.Domain.Common;
namespace Ai.Domain.Messages.ValueObjects
{
    public sealed record MessageContent
    {
        public string Value { get; }

        private MessageContent(string value)
        {
            Value = value;
        }

        public static MessageContent Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException(
                    "Nội dung tin nhắn không được để trống.");

            value = value.Trim();

            if (value.Length > 50_000)
                throw new DomainException(
                    "Nội dung tin nhắn vượt quá giới hạn.");

            return new MessageContent(value);
        }

        public override string ToString() => Value;
    }
}
