using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonDiceMonsters
{
    public class rawcardinfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public string attribute { get; set; }
        public string type { get; set; }
        public string category { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public int lp { get; set; }
        public string cardtext { get; set; }
        public string setpack { get; set; }
        public string rarity { get; set; }
        public List<rawdiceinfo> diceinforaw = new List<rawdiceinfo>();     
    }

    public class rawdiceinfo
    {
        public rawdiceinfo(string lv, string cr1, string va1, string cr2, string va2, string cr3, string va3,
                                      string cr4, string va4, string cr5, string va5, string cr6, string va6)
        {
            level = lv;
            crest1 = cr1;
            crest2 = cr2;
            crest3 = cr3;            
            crest4 = cr4;           
            crest5 = cr5;
            crest6 = cr6;
            value1 = va1;
            value2 = va2;
            value3 = va3;
            value4 = va4;
            value5 = va5;
            value6 = va6;

        }
        public string level { get; set; }
        public string crest1 { get; set; }
        public string value1 { get; set; }
        public string crest2 { get; set; }
        public string value2 { get; set; }
        public string crest3 { get; set; }
        public string value3 { get; set; }
        public string crest4 { get; set; }
        public string value4 { get; set; }
        public string crest5 { get; set; }
        public string value5 { get; set; }
        public string crest6 { get; set; }
        public string value6 { get; set; }
    }
}
