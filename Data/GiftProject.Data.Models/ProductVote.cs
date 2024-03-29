﻿namespace GiftProject.Data.Models
{
    using System;

    using GiftProject.Data.Common.Models;
    using GiftProject.Data.Models.Enumeration;

    public class ProductVote : BaseDeletableModel<int>
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public VoteType Vote { get; set; }

        public DateTime NextVoteDate { get; set; }
    }
}
