namespace MoneyPlanner.Service.DTO
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public int Volume { get; set; }
        public string Date { get; set; }
        public int UserId { get; set; }
    }
}