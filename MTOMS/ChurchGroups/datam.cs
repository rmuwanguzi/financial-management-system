using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdaHelperManager;

using System.Windows.Forms;
using SdaHelperManager.Security;
using SdaHelperManager;
namespace MTOMS
{
    partial class datam
    {
        public static SortedList<int, ic.church_groupC> DATA_CHURCH_GROUPS { get; set; }
        public static SortedList<int, ic.church_group_typeC> DATA_CG_TYPES { get; set; }
        public static void fill_church_group_types(xing xd)
        {
            string _str = string.Empty;
            string _table_name = "church_group_types_tb";
            if (DATA_CG_TYPES == null)
            {
                datam.DATA_CG_TYPES = new SortedList<int, ic.church_group_typeC>();
            }
            if (wdata.TABLE_STAMP == null)
            {
                wdata.TABLE_STAMP = new SortedList<string, long>();
            }
            if (wdata.TABLE_STAMP.IndexOfKey(_table_name) == -1)
            {
                wdata.TABLE_STAMP.Add(_table_name, 0);
            }
            bool is_new = false;
            bool load_all = false;
            var _stamp = xd.GetTimeStamp(_table_name);
            if (DATA_CG_TYPES.Keys.Count == 0)
            {
                _str = "select * from " + _table_name;
                load_all = true;
            }
            else
            {
                if (wdata.TABLE_STAMP[_table_name] == _stamp)
                {
                    return;
                }
                _str = string.Format("select * from " + _table_name + " where fs_time_stamp > {0}", wdata.TABLE_STAMP[_table_name]);
            }
            wdata.TABLE_STAMP[_table_name] = _stamp;
            ic.church_group_typeC _obj = null;
            #region database fill
            try
            {
                using (var _dr = xd.SelectCommand(_str))
                {
                    while (_dr.Read())
                    {
                        _obj = null;
                        if (load_all)
                        {
                            _obj = new ic.church_group_typeC();
                            is_new = true;
                        }
                        else
                        {
                            try
                            {
                                _obj = datam.DATA_CG_TYPES[_dr["cg_type_id"].ToInt32()];
                                is_new = false;
                            }
                            catch (Exception ex)
                            {
                                if (_obj == null)
                                {
                                    _obj = new ic.church_group_typeC();
                                    is_new = true;
                                }
                            }
                        }
                        if (is_new)
                        {
                            _obj.cg_type_id = _dr["cg_type_id"].ToInt32();
                            datam.DATA_CG_TYPES.Add(_obj.cg_type_id, _obj);
                        }
                        _obj.cg_type_name = _dr["cg_type_name"].ToStringNullable();
                        _obj.sys_account_id = _dr["sys_account_id"].ToInt32();
                        _obj.is_default = _dr["is_default"].ToInt32() == 0 ? false : true;
                    }
                    _dr.Close(); _dr.Dispose();
                }




            #endregion
                if (datam.DATA_CG_TYPES.Count == 0)
                {
                    //no db records
                    datam.DATA_CG_TYPES = new SortedList<int, ic.church_group_typeC>();
                    string[] _types = new string[] { "Family", "City", "Building Class", "Lesson Class", "Prayer Group", "FellowShip Group", "Cell", "Bible Class", "Cooking Class" };
                    int j = -200;
                    foreach (var k in _types)
                    {
                        datam.DATA_CG_TYPES.Add(j, new ic.church_group_typeC()
                        {
                            cg_type_id = j,
                            cg_type_name = k
                        });
                        
                        xd.SingleInsertCommandOnDuplicate(_table_name, new string[]
                    {
                        "cg_type_id",
                        "cg_type_name",
                        "fs_time_stamp",
                        "lch_id"
                    }, new object[] { datam.DATA_CG_TYPES[j].cg_type_id, datam.DATA_CG_TYPES[j].cg_type_name, 0, sdata.ChurchID },
                        string.Format("ON DUPLICATE KEY UPDATE fs_time_stamp={0}", SQLH.UnixStamp));
                        j++;
                    }
                }
                if (load_all)
                {
                    var _keys = (from k in xso.xso.DATA_MEM_COL_DET.Values
                                 where k.col_cat_id == 13
                                 select k.col_id).ToList();
                    foreach (var k in _keys)
                    {
                        xso.xso.DATA_MEM_COL_DET.Remove(k);
                    }
                    int j = -1000;
                    short disp_index = 1;
                    foreach (var t in datam.DATA_CG_TYPES.Values)
                    {
                        xso.xso.DATA_MEM_COL_DET.Add(j, new xso.ic.Mem_col_det()
                        {
                            col_cat_id = 13,
                            col_id = j,
                            col_name = t.cg_type_name,
                            disp_index = disp_index
                        });
                        t.col_id = j;
                        disp_index++;
                        j++;
                    }
                }
                else
                {
                    var _col = (from k in xso.xso.DATA_MEM_COL_DET.Values
                                    where k.col_cat_id == 13
                                    orderby k.col_id descending
                                    select k).FirstOrDefault();
                    int j = _col.col_id + 1;
                    short disp_index = (_col.disp_index + 1).ToInt16();
                    foreach (var t in datam.DATA_CG_TYPES.Values.Where(f => f.col_id == 0))
                    {
                        xso.xso.DATA_MEM_COL_DET.Add(j, new xso.ic.Mem_col_det()
                        {
                            col_cat_id = 13,
                            col_id = j,
                            col_name = t.cg_type_name,
                            disp_index = disp_index
                        });
                        t.col_id = j;
                        disp_index++;
                        j++;
                    }
                }
            }
            catch ( VistaDB.Diagnostic.VistaDBException ex)
            {
               
                MessageBox.Show(ex.Message);
                throw new Exception("Data Loading Failed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void fill_church_groups(xing xd)
        {
            string _str = string.Empty;
            string _table_name = "church_group_tb";
            if (DATA_CHURCH_GROUPS == null)
            {
                datam.DATA_CHURCH_GROUPS = new SortedList<int, ic.church_groupC>();
            }
            if (wdata.TABLE_STAMP == null)
            {
                wdata.TABLE_STAMP = new SortedList<string, long>();
            }
            if (wdata.TABLE_STAMP.IndexOfKey(_table_name) == -1)
            {
                wdata.TABLE_STAMP.Add(_table_name, 0);
            }
            bool is_new = false;
            bool load_all = false;
            var _stamp = xd.GetTimeStamp(_table_name);
            if (DATA_CHURCH_GROUPS.Keys.Count == 0)
            {
                _str = "select * from " + _table_name;
                load_all = true;
            }
            else
            {
                if (wdata.TABLE_STAMP[_table_name] == _stamp)
                {
                    return;
                }
                _str = string.Format("select * from " + _table_name + " where fs_time_stamp > {0}", wdata.TABLE_STAMP[_table_name]);
            }
            wdata.TABLE_STAMP[_table_name] = _stamp;
            ic.church_groupC _obj = null;
           
            try
            {
                using (var _dr = xd.SelectCommand(_str))
                {
                    while (_dr.Read())
                    {
                        _obj = null;
                        if (load_all)
                        {
                            _obj = new ic.church_groupC();
                            is_new = true;
                        }
                        else
                        {
                            try
                            {
                                _obj = datam.DATA_CHURCH_GROUPS[_dr["cg_id"].ToInt32()];
                                is_new = false;
                            }
                            catch (Exception ex)
                            {
                                if (_obj == null)
                                {
                                    _obj = new ic.church_groupC();
                                    is_new = true;
                                }
                            }
                        }
                        if (is_new)
                        {
                            _obj.cg_type_id = _dr["cg_type_id"].ToInt32();
                            _obj.cg_id = _dr["cg_id"].ToInt32();
                            _obj.cg_type_id = _dr["cg_type_id"].ToInt16();
                           datam.DATA_CHURCH_GROUPS.Add(_obj.cg_id, _obj);
                        }
                        _obj.cg_name = _dr["cg_name"].ToStringNullable();
                        _obj.sys_account_id = _dr["sys_account_id"].ToInt32();
                    }
                    _dr.Close(); _dr.Dispose();
                }
                if (load_all)
                {
                    _str = "select * from church_group_members_tb where mem_status=1";
                    using (var _dr = xd.SelectCommand(_str))
                    {
                        while (_dr.Read())
                        {
                            try
                            {
                                datam.DATA_MEMBER[_dr["mem_id"].ToInt32()].ChurchGroupCollection.Add(datam.DATA_CHURCH_GROUPS[_dr["cg_id"].ToInt32()]);
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                        _dr.Close(); _dr.Dispose();
                    }
                }

            }
            catch ( VistaDB.Diagnostic.VistaDBException ex)
            {
                MessageBox.Show(ex.Message);
                throw new Exception("Data Loading Failed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private static void fill_church_group_types()
        {
            if (datam.DATA_CG_TYPES == null)
            {
                datam.DATA_CG_TYPES = new SortedList<int, ic.church_group_typeC>();
                string[] _types = new string[] { "Family", "City", "Building Class", "Lesson Class", "Prayer Group", "FellowShip Group", "Cell", "Bible Class", "Cooking Class" };
                int j = -200;
                foreach (var k in _types)
                {
                    datam.DATA_CG_TYPES.Add(j, new ic.church_group_typeC()
                    {
                        cg_type_id = j,
                        cg_type_name = k
                    });
                    j++;
                }
              //  int _max_value = xso.xso.DATA_MEM_COL_DET.Keys.Max();
                var _keys = (from k in xso.xso.DATA_MEM_COL_DET.Values
                             where k.col_cat_id == 13
                             select k.col_id).ToList();
                foreach (var k in _keys)
                {
                    xso.xso.DATA_MEM_COL_DET.Remove(k);
                }
                j = -1000;
                short disp_index = 1;
                foreach (var t in datam.DATA_CG_TYPES.Values)
                {
                    xso.xso.DATA_MEM_COL_DET.Add(j, new xso.ic.Mem_col_det()
                    {
                        col_cat_id = 13,
                        col_id = j,
                        col_name = t.cg_type_name,
                        disp_index = disp_index
                    });
                    t.col_id = j;
                    disp_index++;
                    j++;
                }
            }
        }
    }
}
