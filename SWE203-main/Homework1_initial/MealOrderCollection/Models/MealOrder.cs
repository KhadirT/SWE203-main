 public class MealOrder
    {
        public string OrderID { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string MealName { get; set; } = string.Empty;
        public bool Collected { get; set; }
        public DateTime? CollectedAt { get; set; }
    }