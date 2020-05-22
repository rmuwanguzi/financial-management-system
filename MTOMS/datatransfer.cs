using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdaHelperManager;
using SdaHelperManager.Security;

namespace MTOMS
{
   public  class datatransfer
    {
      private static string[] _months = new string[] { "jan", "feb", "mar", "apr", "may", "jun", "jul", "aug", "sep", "oct", "nov", "dec" };
      public static string mem_code = null;
      public static int mem_id = 0;
      public static void GetMemberID(xing xd)
      {
          mem_id = xd.IDCtrlGet(mem_id_tb, 2012, 1001);
          if (mem_id == 666 || mem_id == 1000)
          {
              xd.IDCtrlDelete(mem_id_tb);
              mem_id = dbm.IDCtrlGet(mem_id_tb, 2012, 1001);
          }
          mem_code = fn.GetMemberCode(mem_id);
          mem_id = (string.Format("{0}{1}", datam.LCH_ID, mem_id)).ToInt32();

      }
      static string  mem_id_tb = "id_membership";

      private static bool process_cell_bunga(ic.memberC _emp, string _alpha, string _val)
      {
          try
          {
              #region start
              switch (_alpha.ToLower())
              {
                  case "a":
                      {
                          var _names = _val.Trim().ToProperCase().Split(new char[] { ' ' });
                          if (_names.Length == 1)
                          {
                              _emp.mem_name = string.Format("{0}", _names[0]);
                          }
                          if (_names.Length == 2)
                          {
                              _emp.mem_name = string.Format("{0} {1}", _names[0], _names[1]);
                          }
                          if (_names.Length == 3)
                          {
                              _emp.mem_name = string.Format("{0} {1}", _names[0], _names[1]);
                              _emp.mem_o_name = string.Format("{0}", _names[2]);
                          }
                          if (_names.Length == 4)
                          {
                              _emp.mem_name = string.Format("{0} {1}", _names[0], _names[1]);
                              _emp.mem_o_name = string.Format("{0} {1}", _names[2], _names[3]);
                          }
                          break;
                      }
                  case "b":
                      {
                          _emp.mem_o_name += _val.Trim().ToProperCase();
                          break;
                      }
                  case "c":
                      {
                          var _new_val = _val.Trim();
                          if (string.IsNullOrEmpty(_new_val))//nil
                          {
                              break;
                          }
                          _emp.temp_date = _new_val;
                          break;
                      }
                  case "d"://birth_year
                      {
                          var _new_val = _val.Trim();
                          if (string.IsNullOrEmpty(_new_val) || _new_val.Length < 4)//nil
                          {
                              return false;
                          }
                          _emp.birth_yr = _new_val.ToInt16();
                          string _dt_string = null;
                          if (string.IsNullOrEmpty(_emp.temp_date))
                          {
                              return true;
                          }
                          else
                          {
                              var _split = _emp.temp_date.Split(new char[] { '-' });
                              if (_split.Length == 1)
                              {
                                  return true;
                              }
                              else
                              {
                                  short _id = 0;
                                  bool _found = false;
                                  foreach (var _m in _months)
                                  {
                                      _id++;
                                      if (_split[1].ToLower() == _m)
                                      {
                                          _found = true;
                                          break;
                                      }
                                  }
                                  if (!_found)
                                  {
                                      dbm.MessageNormal(_split[1], "er");
                                      break;
                                  }
                                  _dt_string = string.Format("{0}/{1}/{2}", _id, _split[0].Trim().ToInt16(), _new_val.ToInt16());
                              }

                          }
                          if (!string.IsNullOrEmpty(_dt_string))
                          {
                              _emp.birth_yr = _new_val.ToInt16();
                              // _emp.mem_birth_date = _dt_string;
                              _emp.mem_birth_fs_id = fn.GetFSID(_dt_string);
                          }
                          break;
                      }
                  case "e"://gender
                      {
                          _emp.gender_id = _val.Trim().ToLower() == "m" ? em.xgender.Male.ToByte() : em.xgender.Female.ToByte();
                          break;
                      }
                  case "f":
                      {
                          var _new_val = _val.Trim();
                          if (string.IsNullOrEmpty(_new_val))//nil
                          {
                              break;
                          }
                          if (_emp.objParent == null)
                          {
                              _emp.objParent = new MTOMS.ic.parentC();
                              _emp.objParent.father_name = _new_val.ToProperCase();
                          }
                          break;
                      }
                  case "g":
                      {
                          var _new_val = _val.Trim();
                          if (string.IsNullOrEmpty(_new_val))//nil
                          {
                              break;
                          }
                          if (_emp.objParent == null)
                          {
                              _emp.objParent = new MTOMS.ic.parentC();
                              _emp.objParent.mother_name = _new_val.ToProperCase();
                          }
                          break;
                      }
                  case "h":
                      {
                          var _new_val = _val.Trim().ToLower();
                          if (string.IsNullOrEmpty(_new_val))//nil
                          {
                              _emp.country_id = xso.xso.DATA_COUNTRY.Values.Where(k => k.country_name.ToLower() == "uganda").FirstOrDefault().country_id;
                              break;
                          }
                          _emp.country_id = xso.xso.DATA_COUNTRY.Values.Where(k => k.country_name.ToLower() == _new_val).FirstOrDefault().country_id;
                          if (_emp.country_id == 0)
                          {
                              dbm.MessageNormal(_new_val, "er");
                          }
                          break;
                      }
                  case "i"://residence
                      {
                          var _new_val = _val.Trim().ToProperCase();
                          if (string.IsNullOrEmpty(_new_val))//nil
                          {
                              break;
                          }
                          if (_emp.objAddress == null)
                          {
                              _emp.objAddress = new MTOMS.ic.addressC();
                          }
                          _emp.objAddress.village = _new_val;
                          break;
                      }
                  case "j"://residence
                      {
                          var _new_val = _val.Trim().ToProperCase();
                          if (string.IsNullOrEmpty(_new_val))//nil
                          {
                              break;
                          }
                          if (_emp.objAddress == null)
                          {
                              _emp.objAddress = new MTOMS.ic.addressC();
                          }
                          _emp.objAddress.phy_address = _new_val;
                          break;
                      }
                  case "k"://contact
                      {
                          var _new_val = _val.Trim().ToProperCase();
                          if (string.IsNullOrEmpty(_new_val))//nil
                          {
                              break;
                          }
                          if (_emp.objAddress == null)
                          {
                              _emp.objAddress = new MTOMS.ic.addressC();
                          }
                          var _split = _new_val.Split(new char[] { '/' });
                          if (_split.Length == 1)
                          {
                              _emp.objAddress.phone1 = !_split[0].StartsWith("0") ? string.Format("0{0}", _split[0]) : _split[0];
                          }
                          else
                          {
                              _emp.objAddress.phone1 = !_split[0].StartsWith("0") ? string.Format("0{0}", _split[0]) : _split[0];
                              _emp.objAddress.phone2 = !_split[1].StartsWith("0") ? string.Format("0{0}", _split[1]) : _split[1];
                          }
                          break;
                      }
                  case "l"://email
                      {
                          var _new_val = _val.Trim();
                          if (string.IsNullOrEmpty(_new_val))//nil
                          {
                              break;
                          }
                          if (_emp.objAddress == null)
                          {
                              _emp.objAddress = new MTOMS.ic.addressC();
                          }
                          _emp.objAddress.email = _new_val;
                          break;
                      }
                  case "m"://education level
                      {
                          var _new_val = _val.Trim();
                          if (string.IsNullOrEmpty(_new_val))//nil
                          {
                              _emp.educ_level_id = xso.xso.DATA_COMMON.Values.Where(j => j.item_section == xso.em.common_section.education_level & j.item_name.ToLower() == "none").FirstOrDefault().item_id;

                              break;
                          }
                          _emp.educ_level_id = xso.xso.DATA_COMMON.Values.Where(j => j.item_section == xso.em.common_section.education_level & j.item_name.ToLower() == _new_val.ToLower()).FirstOrDefault().item_id;
                          break;
                      }
                  case "p"://occupation
                      {
                          var _new_val = _val.Trim();
                          if (string.IsNullOrEmpty(_new_val))//nil
                          {
                              break;
                          }
                          if (_emp.objOccupation == null)
                          {
                              _emp.objOccupation = new MTOMS.ic.occupationC();
                          }
                          _emp.objOccupation.occupation_other = _new_val;
                          break;
                      }
                  case "q"://education level
                      {
                          var _new_val = _val.Trim();
                          if (string.IsNullOrEmpty(_new_val))//nil
                          {
                              _emp.marital_type_id = xso.xso.DATA_COMMON.Values.Where(j => j.item_section == xso.em.common_section.marital_type & j.item_name.ToLower() == "single").FirstOrDefault().item_id;

                              break;
                          }
                          _emp.marital_type_id = xso.xso.DATA_COMMON.Values.Where(j => j.item_section == xso.em.common_section.marital_type & j.item_name.ToLower() == _new_val.ToLower()).FirstOrDefault().item_id;
                          break;
                      }
                  case "r"://baptised year
                      {
                          var _new_val = _val.Trim();
                          if (string.IsNullOrEmpty(_new_val))//nil
                          {
                              break;
                          }
                          if (_emp.objBaptism == null)
                          {
                              _emp.objBaptism = new MTOMS.ic.baptismC();
                          }
                          _emp.objBaptism.bapt_yr = _new_val.ToInt16();
                          _emp.baptismal_type_id = 125;
                          break;
                      }
                  case "s"://baptised place
                      {
                          var _new_val = _val.Trim().ToProperCase();
                          if (string.IsNullOrEmpty(_new_val))//nil
                          {
                              break;
                          }
                          if (_emp.objBaptism == null)
                          {
                              _emp.objBaptism = new MTOMS.ic.baptismC();
                          }
                          _emp.objBaptism.bapt_place = _new_val;
                          _emp.baptismal_type_id = 125;
                          break;
                      }
                  case "t"://baptised pastor
                      {
                          var _new_val = _val.Trim().ToProperCase();
                          if (string.IsNullOrEmpty(_new_val))//nil
                          {
                              break;
                          }
                          if (_emp.objBaptism == null)
                          {
                              _emp.objBaptism = new MTOMS.ic.baptismC();
                          }
                          _emp.objBaptism.bapt_pastor = _new_val;
                          _emp.baptismal_type_id = 125;
                          break;
                      }
              }
              #endregion
          }
          catch (Exception ex)
          {
              dbm.ErrorMessage(ex.Message, "jk");
              dbm.MessageNormal(_alpha, "er");
              return false;
          }
          return true;
      }
      private static bool ProcessCell(ic.memberC _emp, string _alpha, string _val)
       {
           try
           {
               #region start
               switch (_alpha.ToLower())
               {
                   case "a":
                       {
                           var _names = _val.Trim().ToProperCase().Split(new char[] { ' ' });
                           if (_names.Length == 1)
                           {
                               _emp.mem_name = string.Format("{0}", _names[0]);
                           }
                           if (_names.Length == 2)
                           {
                               _emp.mem_name = string.Format("{0} {1}", _names[0], _names[1]);
                           }
                           if (_names.Length == 3)
                           {
                               _emp.mem_name = string.Format("{0} {1}", _names[0], _names[1]);
                               _emp.mem_o_name = string.Format("{0}", _names[2]);
                           }
                           if (_names.Length == 4)
                           {
                               _emp.mem_name = string.Format("{0} {1}", _names[0], _names[1]);
                               _emp.mem_o_name = string.Format("{0} {1}", _names[2], _names[3]);
                           }
                           break;
                       }
                   case "b":
                       {
                           _emp.mem_o_name += _val.Trim().ToProperCase();
                           break;
                       }
                   case "c":
                       {
                           var _new_val = _val.Trim();
                           if (string.IsNullOrEmpty(_new_val))//nil
                           {
                               break;
                           }
                           _emp.temp_date = _new_val;
                           break;
                       }
                   case "d"://birth_year
                       {
                           var _new_val = _val.Trim();
                           if (string.IsNullOrEmpty(_new_val) || _new_val.Length<4)//nil
                           {
                               return false;
                           }
                           _emp.birth_yr = _new_val.ToInt16();
                           string _dt_string = null;
                           if (string.IsNullOrEmpty(_emp.temp_date))
                           {
                               return true;
                           }
                           else
                           {
                               var _split = _emp.temp_date.Split(new char[] { '-' });
                               if (_split.Length == 1)
                               {
                                   return true;
                               }
                               else
                               {
                                   short _id = 0;
                                   bool _found = false;
                                   foreach (var _m in _months)
                                   {
                                       _id++;
                                       if (_split[1].ToLower() == _m)
                                       {
                                           _found = true;
                                           break;
                                       }
                                   }
                                   if (!_found)
                                   {
                                       dbm.MessageNormal(_split[1], "er");
                                       break;
                                   }
                                   _dt_string = string.Format("{0}/{1}/{2}", _id, _split[0].Trim().ToInt16(), _new_val.ToInt16());
                               }

                           }
                           if (!string.IsNullOrEmpty(_dt_string))
                           {
                               _emp.birth_yr = _new_val.ToInt16();
                              // _emp.mem_birth_date = _dt_string;
                               _emp.mem_birth_fs_id = fn.GetFSID(_dt_string);
                           }
                           break;
                       }
                   case "e"://gender
                       {
                           _emp.gender_id = _val.Trim().ToLower() == "m" ? em.xgender.Male.ToByte() : em.xgender.Female.ToByte();
                           break;
                       }
                   case "f":
                       {
                           var _new_val = _val.Trim();
                           if (string.IsNullOrEmpty(_new_val))//nil
                           {
                               break;
                           }
                           if (_emp.objParent == null)
                           {
                               _emp.objParent = new MTOMS.ic.parentC();
                               _emp.objParent.father_name = _new_val.ToProperCase();
                           }
                           break;
                       }
                   case "g":
                       {
                           var _new_val = _val.Trim();
                           if (string.IsNullOrEmpty(_new_val))//nil
                           {
                               break;
                           }
                           if (_emp.objParent == null)
                           {
                               _emp.objParent = new MTOMS.ic.parentC();
                               _emp.objParent.mother_name = _new_val.ToProperCase();
                           }
                           break;
                       }
                   case "h":
                       {
                           var _new_val = _val.Trim().ToLower();
                           if (string.IsNullOrEmpty(_new_val))//nil
                           {
                               _emp.country_id = xso.xso.DATA_COUNTRY.Values.Where(k => k.country_name.ToLower() == "uganda").FirstOrDefault().country_id;
                               break;
                           }
                           _emp.country_id = xso.xso.DATA_COUNTRY.Values.Where(k => k.country_name.ToLower() == _new_val).FirstOrDefault().country_id;
                           if (_emp.country_id == 0)
                           {
                               dbm.MessageNormal(_new_val, "er");
                           }
                           break;
                       }
                   case "i"://residence
                       {
                           var _new_val = _val.Trim().ToProperCase();
                           if (string.IsNullOrEmpty(_new_val))//nil
                           {
                               break;
                           }
                           if (_emp.objAddress == null)
                           {
                               _emp.objAddress = new MTOMS.ic.addressC();
                           }
                           _emp.objAddress.village = _new_val;
                           break;
                       }
                   case "j"://residence
                       {
                           var _new_val = _val.Trim().ToProperCase();
                           if (string.IsNullOrEmpty(_new_val))//nil
                           {
                               break;
                           }
                           if (_emp.objAddress == null)
                           {
                               _emp.objAddress = new MTOMS.ic.addressC();
                           }
                           _emp.objAddress.phy_address = _new_val;
                           break;
                       }
                   case "k"://contact
                       {
                           var _new_val = _val.Trim().ToProperCase();
                           if (string.IsNullOrEmpty(_new_val))//nil
                           {
                               break;
                           }
                           if (_emp.objAddress == null)
                           {
                               _emp.objAddress = new MTOMS.ic.addressC();
                           }
                           var _split = _new_val.Split(new char[] { '/' });
                           if (_split.Length == 1)
                           {
                               _emp.objAddress.phone1 = !_split[0].StartsWith("0") ? string.Format("0{0}", _split[0]) : _split[0];
                           }
                           else
                           {
                               _emp.objAddress.phone1 = !_split[0].StartsWith("0") ? string.Format("0{0}", _split[0]) : _split[0];
                               _emp.objAddress.phone2 = !_split[1].StartsWith("0") ? string.Format("0{0}", _split[1]) : _split[1];
                           }
                           break;
                       }
                   case "l"://email
                       {
                           var _new_val = _val.Trim();
                           if (string.IsNullOrEmpty(_new_val))//nil
                           {
                               break;
                           }
                           if (_emp.objAddress == null)
                           {
                               _emp.objAddress = new MTOMS.ic.addressC();
                           }
                           _emp.objAddress.email = _new_val;
                           break;
                       }
                   case "m"://education level
                       {
                           var _new_val = _val.Trim();
                           if (string.IsNullOrEmpty(_new_val))//nil
                           {
                               _emp.educ_level_id = xso.xso.DATA_COMMON.Values.Where(j => j.item_section == xso.em.common_section.education_level & j.item_name.ToLower() == "none").FirstOrDefault().item_id;

                               break;
                           }
                           _emp.educ_level_id = xso.xso.DATA_COMMON.Values.Where(j => j.item_section == xso.em.common_section.education_level & j.item_name.ToLower() == _new_val.ToLower()).FirstOrDefault().item_id;
                           break;
                       }
                   case "p"://occupation
                       {
                           var _new_val = _val.Trim();
                           if (string.IsNullOrEmpty(_new_val))//nil
                           {
                               break;
                           }
                           if (_emp.objOccupation == null)
                           {
                               _emp.objOccupation = new MTOMS.ic.occupationC();
                           }
                           _emp.objOccupation.occupation_other = _new_val;
                           break;
                       }
                   case "q"://education level
                       {
                           var _new_val = _val.Trim();
                           if (string.IsNullOrEmpty(_new_val))//nil
                           {
                               _emp.marital_type_id = xso.xso.DATA_COMMON.Values.Where(j => j.item_section == xso.em.common_section.marital_type & j.item_name.ToLower() == "single").FirstOrDefault().item_id;

                               break;
                           }
                           _emp.marital_type_id = xso.xso.DATA_COMMON.Values.Where(j => j.item_section == xso.em.common_section.marital_type & j.item_name.ToLower() == _new_val.ToLower()).FirstOrDefault().item_id;
                           break;
                       }
                   case "r"://baptised year
                       {
                           var _new_val = _val.Trim();
                           if (string.IsNullOrEmpty(_new_val))//nil
                           {
                               break;
                           }
                           if (_emp.objBaptism == null)
                           {
                               _emp.objBaptism = new MTOMS.ic.baptismC();
                           }
                           _emp.objBaptism.bapt_yr = _new_val.ToInt16();
                           _emp.baptismal_type_id = 125;
                           break;
                       }
                   case "s"://baptised place
                       {
                           var _new_val = _val.Trim().ToProperCase();
                           if (string.IsNullOrEmpty(_new_val))//nil
                           {
                               break;
                           }
                           if (_emp.objBaptism == null)
                           {
                               _emp.objBaptism = new MTOMS.ic.baptismC();
                           }
                           _emp.objBaptism.bapt_place = _new_val;
                           _emp.baptismal_type_id = 125;
                           break;
                       }
                   case "t"://baptised pastor
                       {
                           var _new_val = _val.Trim().ToProperCase();
                           if (string.IsNullOrEmpty(_new_val))//nil
                           {
                               break;
                           }
                           if (_emp.objBaptism == null)
                           {
                               _emp.objBaptism = new MTOMS.ic.baptismC();
                           }
                           _emp.objBaptism.bapt_pastor = _new_val;
                           _emp.baptismal_type_id = 125;
                           break;
                       }
               }
               #endregion
           }
           catch (Exception ex)
           {
               dbm.ErrorMessage(ex.Message,"jk");
               dbm.MessageNormal(_alpha, "er");
               return false;
           }
           return true;
       }
       //
       public static bool Save_member(xing xd, ic.memberC mem_obj)
       {
           if (xd == null) { return false; }
           if (mem_obj == null) { return false; }
          
           string[] tb_col = null;
           object[] _row = null;
           object _bdate = null;
           object _jdate = null;
           //if (!string.IsNullOrEmpty(mem_obj.mem_birth_date))
           //{
           //    _bdate = Convert.ToDateTime(mem_obj.mem_birth_date);
           //}
           GetMemberID(xd);
           mem_obj.mem_id = mem_id;
           mem_obj.mem_code = mem_code;
           mem_obj.mem_u_code = string.Format("{0}-{1}", mem_obj.mem_code, datam.LCH_ID);
           mem_obj.mem_status_id = em.xmem_status.Normal.ToByte();
           mem_obj.country_id = -1;
           if (mem_obj.join_date!=null)
           {
                _jdate = mem_obj.join_date.Value;
           }
           Int64 _stamp = xd.CreateFsTimeStamp();
           tb_col = new string[]
            {
           #region MyRegion
		    "mem_id",
            "mem_code",
            "mem_u_code",
            "mem_title_id",
            "mem_name",
            "mem_o_name",
            "mem_gender_id",
            "mem_birth_yr",
            "mem_birth_date",
            "mem_birth_fs_id",
            "marital_type_id",
            "e_fs_id",
            "e_date",
            "baptismal_type_id",
            "mem_status_type_id",
            "mem_educ_level_id",
            "prev_church",
            "lch_type_id",
            "lch_id",
            "exp_type",
            "country_id",
            "tribe_id",
            "join_year",
            "join_date",
            "join_fs_id",
            "national_id",
            "national_id_type",
            "fs_time_stamp",
            "pc_us_id",
            "xfield1",
            "xfield2",
            "mem_type_id",
            "prev_religion",
            "empl_status_id",
            "mem_church_code"
	        #endregion
            };

           _row = new object[]
            {
           #region MyRegion
		    mem_obj.mem_id,
            mem_obj.mem_code,
            mem_obj.mem_u_code,
            mem_obj.mem_title_id,
            mem_obj.mem_name,
            mem_obj.mem_o_name,
            mem_obj.gender_id,
            mem_obj.birth_yr,
           _bdate,
            mem_obj.mem_birth_fs_id,
            mem_obj.marital_type_id,
            datam.CURR_FS.fs_id,
            datam.CURR_DATE,
            mem_obj.baptismal_type_id,
            mem_obj.mem_status_id,
            mem_obj.educ_level_id,
            mem_obj.prev_church,
            datam.LCH_TYPE_ID,
            datam.LCH_ID,
            emm.export_type.insert.ToByte(),
            mem_obj.country_id,
            0,//tribe name
            mem_obj.join_year,
           _jdate,
            mem_obj.join_fs_id,
            0,// national id
            0,//national_id_type
            _stamp,
            datam.PC_US_ID,
            0,
            0,
            mem_obj.mem_type.ToByte(),
            mem_obj.prev_religion,
            mem_obj.employment_status.ToByte(),
            mem_obj.mem_church_code
	#endregion
           };
           xd.SingleInsertCommand("member_tb", tb_col, _row);
           xd.IDCtrlDelete(mem_id_tb);
           return true;
       }
       public static bool Save_address(xing xd, ic.memberC mem_obj)
       {
           if (xd == null) { return false; }
           if (mem_obj == null) { return false; }
           ic.addressC objAddress = mem_obj.objAddress;
           objAddress.mem_id = mem_obj.mem_id;
           string[] tb_col = null;
           object[] _row = null;

           tb_col = new string[]
            {
            "mem_id","phone1",
            "phone2","email",
            "phy_address","postal_address",
            "exp_type","division",
            "village","lch_id",
            "lch_type_id"
            };

           _row = new object[]
            {
            objAddress.mem_id,
            objAddress.phone1,
            objAddress.phone2,
            objAddress.email,
            objAddress.phy_address,
            objAddress.postal_address,
            emm.export_type.insert.ToByte(),
            objAddress.division,
            objAddress.village,
            datam.LCH_ID,
            datam.LCH_TYPE_ID
            };
            xd.SingleInsertCommandInt("address_tb", tb_col, _row);
         
           return true;
       }
       private static bool Save_baptism(xing xd, ic.memberC mem_obj)
       {
           if (xd == null) { return false; }
           if (mem_obj == null) { return false; }
           ic.baptismC objBaptism = mem_obj.objBaptism;
           objBaptism.mem_id = mem_obj.mem_id;
           string[] tb_col = null;
           object[] _row = null;

           tb_col = new string[]
            {
           "mem_id","bapt_yr",
           "bapt_date","bapt_fs_id","bapt_place",
           "bapt_pastor","bapt_country",
           "bapt_church","exp_type",
           "lch_id","lch_type_id",
           "bapt_pastor_id",
           "bapt_church_id"
            };

           _row = new object[]
            {
            mem_obj.mem_id,
            objBaptism.bapt_yr,
            null,
            objBaptism.bapt_fs_id,
            objBaptism.bapt_place,
            objBaptism.bapt_pastor,
            objBaptism.bapt_country,
            objBaptism.bapt_church,
            emm.export_type.insert.ToByte(),
            datam.LCH_ID,
            datam.LCH_TYPE_ID,
            objBaptism.bapt_pastor_id,
            objBaptism.bapt_church_id
            };
           objBaptism.un_id = xd.SingleInsertCommandInt("baptismal_tb", tb_col, _row);
           
           return true;
       }
       private static bool Save_occupation(xing xd, ic.memberC mem_obj)
       {
           if (xd == null) { return false; }
           if (mem_obj == null) { return false; }
           string[] tb_col = null;
           object[] _row = null;

           tb_col = new string[]
            {
            "mem_id",
            "occup_cat_id",
            "occup_type_id",
            "employer",
            "exp_type",
            "lch_id",
            "lch_type_id",
            "occup_other",
            "other_skills",
            };

           _row = new object[]
            {
            mem_obj.mem_id,
            mem_obj.objOccupation.occ_cat_id,
            mem_obj.objOccupation.occ_type_id,
            mem_obj.objOccupation.employer,
            emm.export_type.insert.ToByte(),
            datam.LCH_ID,
            datam.LCH_TYPE_ID,
            mem_obj.objOccupation.occupation_other,
            mem_obj.objOccupation.other_skills
            };
          xd.SingleInsertCommandInt("occupation_tb", tb_col, _row);
           return true;
       }
       private static bool Save_parent(xing xd, ic.memberC mem_obj)
       {
           if (xd == null) { return false; }
           if (mem_obj == null) { return false; }
    
           string[] tb_col = null;
           object[] _row = null;

           tb_col = new string[]
            {
            "mem_id",
            "father_name",
            "father_phone",
            "father_status",
            "mother_name",
            "mother_phone",
            "mother_status",
            "guardian_name",
            "guardian_phone",
            "exp_type",
            "father_mem_id",
            "mother_mem_id",
            "guardian_mem_id",
            "lch_id",
            "lch_type_id"
            };
           _row = new object[]
            {
            mem_obj.mem_id,
            mem_obj.objParent.father_name,
            mem_obj.objParent.father_phone,
            mem_obj.objParent.father_status,
            mem_obj.objParent.mother_name,
            mem_obj.objParent.mother_phone,
            mem_obj.objParent.mother_status,
            mem_obj.objParent.guardian_name,
            mem_obj.objParent.guardian_phone,
            emm.export_type.insert.ToByte(),
            mem_obj.objParent.father_mem_id,
            mem_obj.objParent.mother_mem_id,
            mem_obj.objParent.guardian_mem_id,
            datam.LCH_ID,
            datam.LCH_TYPE_ID
            };
          xd.SingleInsertCommand("parents_tb", tb_col, _row);
          return true;
       }
    }
}
