

using System;

namespace FastBot.Messages
{
    public static class MessageExtensions
    {
        /// <summary>
        /// Compare text in message.
        /// </summary>
        public static bool CompareText(this Message message, string textToCompare) =>
            String.Equals(message?.Text, textToCompare, comparisonType: StringComparison.InvariantCultureIgnoreCase);
    }
}