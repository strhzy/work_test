using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace worktest
{
    internal class Order
    {
        public int id {  get; set; }
        public int mass { get; set; }
        public string district { get; set; }
        public string deliverydate { get; set; }

/*        public Order(int id, int mass, string district, string deliverydate)
        {
            this.id = id;
            this.mass = mass;
            this.district = district;
            this.deliverydate = deliverydate;
        }*/
    }
}
