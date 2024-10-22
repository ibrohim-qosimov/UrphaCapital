namespace UrphaCapital.Domain.Entities
{
    public enum EOrderPaymentStatus
    {
        Pending,     // To'lov hali amalga oshirilmagan
        Paid,        // To'lov amalga oshirilgan
        Failed,      // To'lov muvaffaqiyatsiz
        Refunded,    // To'lov qaytarilgan
        Cancelled    // To'lov bekor qilingan
    }
}