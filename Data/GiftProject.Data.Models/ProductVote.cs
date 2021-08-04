﻿using System;

namespace GiftProject.Data.Models
{
    using GiftProject.Data.Common.Models;

    public class ProductVote : BaseDeletableModel<int>
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public int Rate { get; set; }

        public DateTime NextVoteDate { get; set; }
    }
}
