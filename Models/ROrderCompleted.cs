//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCRestaurant27Tem2022.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ROrderCompleted
    {
        public int id_order { get; set; }
        public long id_FD { get; set; }
        public long id_bill { get; set; }
        public int id_waiter { get; set; }
        public System.DateTime odatetime { get; set; }
    
        public virtual FoodDrink FoodDrink { get; set; }
    }
}
