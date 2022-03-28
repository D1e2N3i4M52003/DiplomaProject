﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.JSONModels
{
    public class CommentModel
    {
        public Guid Id { get; set; }
        public string AuthorUsername { get; set; }

        public Guid AuthorId { get; set; }

        public DateTime PostDate { get; set; }

        public string Text { get; set; }
    }
}
