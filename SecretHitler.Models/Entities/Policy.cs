using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SecretHitler.Models.Entities
{
    [Table("Policy")]
    public class Policy
    {
        public int Id { get; set; }

        public int GameId { get; set; }
        public PolicyType PolicyType { get; set; }

        public Policy(PolicyType policyType)
        {
            PolicyType = policyType;
        }

        public Policy()
        {

        }
    }
}
