using System;
using JetBrains.Annotations;

namespace AppliedSystems.Dtos
{
    public class RejectionMessageAndSuccess
    {
        public RejectionMessageAndSuccess([NotNull] string rejectionMessage, bool success)
        {
            if (rejectionMessage == null) throw new ArgumentNullException(nameof(rejectionMessage));

            RejectionMessage = rejectionMessage;
            Success = success;
        }

        [NotNull]
        public string RejectionMessage { get; }

        
        public bool Success { get; }

        public override string ToString()
        {
            return $"{nameof(RejectionMessage)}: {RejectionMessage}, {nameof(Success)}: {Success}";
        }
    }
}