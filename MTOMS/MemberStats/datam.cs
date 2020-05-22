using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdaHelperManager;
namespace MTOMS
{
   public partial class datam
    {
      public static SortedList<string, ic.stat_mem_catC> DATA_MEM_STATS = null;
      private static SortedList<int, string> m_employment_static = null;
      private static SortedList<string, int> m_prev_religion_static = null;   
          
      public static void InitMemberStats()
       {
           m_employment_static = new SortedList<int, string>();
           m_employment_static.Add(13, "None");
           m_employment_static.Add(14, "Employed");
           m_employment_static.Add(15, "UnEmployed");
           m_employment_static.Add(16, "Student");
           m_employment_static.Add(17, "Self Employed");
          //
           m_prev_religion_static = new SortedList<string, int>();
           m_prev_religion_static.Add("Adventist",0);
           m_prev_religion_static.Add("Catholic", 1);
           m_prev_religion_static.Add("Protestant", 2);
           m_prev_religion_static.Add("Muslim",3);
           m_prev_religion_static.Add("Pentecostal",4);
           m_prev_religion_static.Add("Orthodox",5);
           m_prev_religion_static.Add("Jehovah Witness",6);
           int _cat_index = 0;
           DATA_MEM_STATS = new SortedList<string,MTOMS.ic.stat_mem_catC>();
           ic.stat_mem_catC _cat = null;
            _cat = new MTOMS.ic.stat_mem_catC();
           //Gender
           _cat.cat_name = "Gender";
           _cat_index++;
           _cat.cat_index += _cat_index;
           _cat.TypeCollection.Add(123, new MTOMS.ic.stat_mem_typeC()
           {
               type_id = 123,
               type_name = "Male"
           });
           _cat.TypeCollection.Add(124, new MTOMS.ic.stat_mem_typeC()
           {
               type_id = 124,
               type_name = "Female"
           });
           DATA_MEM_STATS.Add(_cat.cat_name, _cat); _cat = null;
           //Baptism
           _cat = new MTOMS.ic.stat_mem_catC();
           _cat.cat_name = "Baptism";
           _cat_index++;
           _cat.cat_index += _cat_index;
           _cat.TypeCollection.Add(125, new MTOMS.ic.stat_mem_typeC()
           {
               type_id = 125,
               type_name = "Baptised"
           });
           _cat.TypeCollection.Add(126, new MTOMS.ic.stat_mem_typeC()
           {
               type_id = 126,
               type_name = "Not Baptised"
           });
           DATA_MEM_STATS.Add(_cat.cat_name, _cat); _cat =null;
           //Age Group
           _cat = new MTOMS.ic.stat_mem_catC();
           _cat.cat_name = "Age Group";
           _cat_index++;
           _cat.cat_index += _cat_index;
           foreach (var r in xso.xso.DATA_AGEGROUP.Values)
           {
               _cat.TypeCollection.Add(r.age_gp_id, new MTOMS.ic.stat_mem_typeC()
               {
                   type_id = r.age_gp_id,
                   type_name = r.age_gp_name
               });
           }
           DATA_MEM_STATS.Add(_cat.cat_name, _cat); _cat = null;
           //
           var nlist = from r in xso.xso.DATA_COMMON.Values
                       where (r.item_section == xso.em.common_section.education_level | r.item_section == xso.em.common_section.marital_type |
                       r.item_section == xso.em.common_section.member_status)
                       orderby r.item_section.ToByte()
                       select r;
           int prev_id = -5;
           foreach (var c in nlist)
           {
               if ( c.item_id == em.xmem_status.Deleted.ToInt16())
               {
                   continue;
               }
               #region init_data
               if (prev_id != c.item_section.ToByte())
               {
                   prev_id = c.item_section.ToByte();
                   _cat = null;
                   _cat = new MTOMS.ic.stat_mem_catC();
                   _cat_index++;
                   _cat.cat_index += _cat_index;
                    #region Common
                   switch (c.item_section)
                   {

                       case xso.em.common_section.education_level:
                           {
                               _cat.cat_name = "Education Level";
                               break;
                           }
                       case xso.em.common_section.marital_type:
                           {
                               _cat.cat_name = "Marital Status";
                               break;
                           }
                       case xso.em.common_section.member_status:
                           {
                               _cat.cat_name = "Member Status";
                               break;
                           }
                   }
                  #endregion
                   DATA_MEM_STATS.Add(_cat.cat_name, _cat);
               }
               #endregion
               _cat.TypeCollection.Add(c.item_id, new MTOMS.ic.stat_mem_typeC()
               {
                   type_name = c.item_name,
                   type_id = c.item_id
               });
            }
           //Nationality
           _cat = new MTOMS.ic.stat_mem_catC();
           _cat.cat_name = "Nationality";
           _cat_index++;
           _cat.cat_index += _cat_index;
           _cat.is_optional = true;
           foreach (var r in xso.xso.DATA_COUNTRY.Values)
           {
               _cat.TypeCollection.Add(r.country_id, new MTOMS.ic.stat_mem_typeC()
               {
                   type_id = r.country_id,
                   type_name = r.country_name
               });
           }
           DATA_MEM_STATS.Add(_cat.cat_name, _cat); _cat = null;
           //Employment Status
           _cat = new MTOMS.ic.stat_mem_catC();
           _cat.cat_name = "Employment Status";
           _cat_index++;
           _cat.cat_index += _cat_index;
           foreach (var r in m_employment_static)
           {
               _cat.TypeCollection.Add(r.Key, new MTOMS.ic.stat_mem_typeC()
               {
                   type_id = r.Key,
                   type_name = r.Value
               });
           }
           DATA_MEM_STATS.Add(_cat.cat_name, _cat); _cat = null;
           //Birth Month
           _cat = new MTOMS.ic.stat_mem_catC();
           _cat.cat_name = "Birth Month";
           _cat_index++;
           _cat.cat_index += _cat_index;
           int _index = 0;
           foreach (var r in datam.MONTHS )
           {
               _index++;
               _cat.TypeCollection.Add(_index, new MTOMS.ic.stat_mem_typeC()
               {
                   type_id = _index,
                   type_name = r
               });
           }
           DATA_MEM_STATS.Add(_cat.cat_name, _cat); _cat = null;
           //Former Religion
           var rel_types = new string[] { "Adventist", "Catholic", "Protestant", "Muslim", "Pentecostal", "Orthodox", "Jehovah Witness" };
           SortedList<int, string> _emmp = new SortedList<int, string>();
           _emmp.Add(0, "Adventist");
           _emmp.Add(1, "Catholic");
           _emmp.Add(2, "Protestant");
           _emmp.Add(3, "Muslim");
           _emmp.Add(4, "Pentecostal");
           _emmp.Add(5, "Orthodox");
           _emmp.Add(6, "Jehovah Witness");
           _cat = new MTOMS.ic.stat_mem_catC();
           _cat.cat_name = "Former Religion";
           _cat_index++;
           _cat.cat_index += _cat_index;
           foreach (var r in _emmp)
           {
               _cat.TypeCollection.Add(r.Key, new MTOMS.ic.stat_mem_typeC()
               {
                   type_id = r.Key,
                   type_name = r.Value
               });
           }
           DATA_MEM_STATS.Add(_cat.cat_name, _cat); _cat = null;
           // Church Groups
           var _grp = from k in datam.DATA_CHURCH_GROUPS.Values
                      group k by k.cg_type_id into new_gp
                      select new
                      {
                          _key = new_gp.Key,
                          obj_one = new_gp.FirstOrDefault(),
                          gp_col = new_gp
                      };
           foreach (var g in _grp)
           {
               _cat = new MTOMS.ic.stat_mem_catC();
               _cat_index++;
               _cat.cat_index += _cat_index;
               _cat.cat_name = datam.DATA_CG_TYPES[g._key].cg_type_name;
               DATA_MEM_STATS.Add(_cat.cat_name, _cat);
               foreach (var r in g.gp_col)
               {
                   _cat.TypeCollection.Add(r.cg_id, new MTOMS.ic.stat_mem_typeC()
                   {
                       type_id = r.cg_id,
                       type_name = r.cg_name
                   });
               }
           }
           _cat = null;
           // Departments
          _cat = new MTOMS.ic.stat_mem_catC();
          _cat_index++;
          _cat.cat_index += _cat_index;
          _cat.cat_name="Departments";
          _cat.is_optional = true;
          foreach (var r in datam.DATA_DEPARTMENT.Values.Where(p => p.dept_type == em.dept_typeS.main))
          {
              _cat.TypeCollection.Add(r.dept_id, new MTOMS.ic.stat_mem_typeC()
              {
                  type_name = r.dept_name,
                  type_id = r.dept_id
              });
          }
          DATA_MEM_STATS.Add(_cat.cat_name, _cat);
          FillMemberStatistic(datam.DATA_MEMBER.Values);
        }
       public static void FillMemberStatistic(IEnumerable<ic.memberC> m_list)
       {
           foreach(var k in datam.DATA_MEM_STATS.Values)
           {
               foreach(var c in k.TypeCollection.Values)
               {
                   c.count=0;
               }
           }
           if (m_list == null)
           {
               return;
           }

           foreach (var n in m_list)
           {
               if (n.mem_status_id == 152) { continue; }
               try
               {
                   datam.DATA_MEM_STATS["Gender"].TypeCollection[n.gender_id].count += 1;
                   datam.DATA_MEM_STATS["Baptism"].TypeCollection[n.baptismal_type_id].count += 1;
                   datam.DATA_MEM_STATS["Age Group"].TypeCollection[n.age_gp_id].count += 1;
                   datam.DATA_MEM_STATS["Nationality"].TypeCollection[n.country_id].count += 1;
                   datam.DATA_MEM_STATS["Education Level"].TypeCollection[n.educ_level_id].count += 1;
                   datam.DATA_MEM_STATS["Marital Status"].TypeCollection[n.marital_type_id].count += 1;
                   datam.DATA_MEM_STATS["Member Status"].TypeCollection[n.mem_status_id].count += 1;
                   if (n.mem_birth_date!=null)
                   {
                       datam.DATA_MEM_STATS["Birth Month"].TypeCollection[n.mem_birth_date.Value.Month].count += 1;
                   }
                   if (!string.IsNullOrEmpty(n.prev_religion))
                   {
                       try
                       {
                           datam.DATA_MEM_STATS["Former Religion"].TypeCollection[m_prev_religion_static[n.prev_religion]].count += 1;
                       }
                       catch (Exception)
                       {
                         
                       }
                   }
                   if (n.objOccupation != null)
                   {
                       datam.DATA_MEM_STATS["Employment Status"].TypeCollection[n.employment_status.ToByte()].count += 1;
                   }
                   if (n.DepartmentCollection.Count > 0)
                   {
                       foreach (var k in n.DepartmentCollection)
                       {
                           datam.DATA_MEM_STATS["Departments"].TypeCollection[k.dept_id].count += 1;
                       }
                   }
                   if (n.ChurchGroupCollection.Count > 0)
                   {
                       string _cat = null;
                       foreach (var k in n.ChurchGroupCollection)
                       {
                           _cat = datam.DATA_CG_TYPES[k.cg_type_id].cg_type_name;
                           if (string.IsNullOrEmpty(_cat))
                           {
                               continue;
                           }
                           datam.DATA_MEM_STATS[_cat].TypeCollection[k.cg_id].count += 1;
                       }
                   }
               }
               catch (Exception ex)
               {
                   System.Windows.Forms.MessageBox.Show(ex.ToString());
               }
                  
           }
       }
    }
}
