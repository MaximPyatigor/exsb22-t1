﻿using BudgetManager.Model;

namespace BudgetManager.SDK.DTOs
{
    public class AddUserDTO
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public Currency? DefaultCurrency { get; set; }
        public Guid? DefaultWallet { get; set; }
        public Country? Country { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Wallet>? Wallets { get; set; }
        public List<Notification>? Notifications { get; set; }
        public List<PiggyBank>? PiggyBanks { get; set; }
    }
}
