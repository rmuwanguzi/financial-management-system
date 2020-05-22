using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTOMS.ic
{
    public class offeringPercentageDistributionC
    {
        public int account_id { get; set; }
        //
        private SortedList<em.off_distribution_type, int> d_data { get; set; }
        public offeringPercentageDistributionC()
        {
            d_data = new SortedList<em.off_distribution_type, int>();
            d_data.Add(em.off_distribution_type.cuc, 0);
            d_data.Add(em.off_distribution_type.district, 0);
            d_data.Add(em.off_distribution_type.lcb, 0);
            d_data.Add(em.off_distribution_type.other, 0);
        }
        public int GetPercentage(em.off_distribution_type _type)
        {
            return d_data[_type];
        }
        public void SetValue(em.off_distribution_type _type, int _val)
        {
            d_data[_type] = _val;
        }

    }
}
