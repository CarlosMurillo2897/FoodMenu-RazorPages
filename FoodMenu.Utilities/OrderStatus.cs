namespace FoodMenu.Utilities
{
    public static class OrderStatus
    {
        public const string StatusPending = "Pending_Payment";
        public const string StatusSubmitted = "Submitted_PaymentApproved";
        public const string StatusRejected = "Rejected_Payment";
        public const string StatusInProgress = "Being Prepared";
        public const string StatusReady = "Ready for Pickup";
        public const string StatusCompleted = "Completed";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefunded = "Refunded";
    }
}
