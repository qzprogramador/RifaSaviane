namespace SavianeRifa.Models
{
    public class PaymentInformation
    {
        public int Id { get; set; }
        // Informação do payload/clipboard do PIX (p.ex. payload ou chave)
        public string PixCopyPaste { get; set; } = string.Empty;
        public string ComprovantePath { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;
        public string TxtId { get; set; } = string.Empty;

        // valor pago (armazenar decimal como string não é ideal; usar decimal para valor)
        public decimal Amount { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.Now;

        // Dados do comprador
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        // Relação 1 (PaymentInformation) -> N (Rifa)
        public ICollection<Rifa> Rifas { get; set; } = new List<Rifa>();
    }

    public class Rifa
    {
        public int Id { get; set; }
        public string Number { get; set; } = string.Empty;
        // Status: disponivel | reservado | vendido
        public string Status { get; set; } = "disponivel";

        public decimal Price { get; set; } = 10.00M;

        // Quando a rifa for paga, ela apontará para a entidade PaymentInformation
        public int? PaymentInformationId { get; set; }
        public PaymentInformation? PaymentInformation { get; set; }
    }
}
