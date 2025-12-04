public enum PaymentStatusEnum
{
    Unpaid = 1,             // Chưa thanh toán
    Paid = 2,               // Đã thanh toán
    Pending = 3,            // Đang chờ thanh toán
    Refunded = 4,           // Hoàn tiền
    PartialOrInstallment = 5 // Thanh toán một phần / trả góp
}
