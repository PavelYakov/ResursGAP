﻿using System.ComponentModel.DataAnnotations;

namespace ResursGAP.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        [Required(ErrorMessage = "Город отправителя обязателен")]
        public string SenderCity { get; set; }
        [Required(ErrorMessage = "Адрес отправителя обязателен")]
        [StringLength(100, ErrorMessage = "Адрес отправителя должен содержать не более 100 символов")]
        public string SenderAddress { get; set; }
        [Required(ErrorMessage = "Город получателя обязателен")]
        
        public string ReceiverCity { get; set; }
        [Required(ErrorMessage = "Город получателя обязателен")]
        [StringLength(100, ErrorMessage = "Адрес получателя должен содержать не более 100 символов")]
        public string ReceiverAddress { get; set; }
        [Required(ErrorMessage = "Введите вес")]
        public double Weight { get; set; }
        [Required(ErrorMessage = "Выберите дату")]
        public DateTime DeliveryDate { get; set; }

        public decimal DeliveryCost { get; set; }

        // Связь с фурой
        public int TruckId { get; set; }
        public Truck Truck { get; set; }

       
    }
}