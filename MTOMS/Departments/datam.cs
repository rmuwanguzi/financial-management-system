using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTOMS
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using SdaHelperManager;
    using System.Windows.Forms;
    
    using SdaHelperManager.Security;

    partial class datam
        {
            public static SortedList<int, ic.departmentC> DATA_DEPARTMENT = null;
            public static SortedDictionary<int, ic.MemRoleC> DATA_ASSN_MEMBER_DEPT = null;
            public static Dictionary<int, string> DEPARTMENT_INNER_COLLECTION = null;
            public static void GetDepartments(xing _xd)
            {
                if (DATA_STAMP_STORE == null) { DATA_STAMP_STORE = new SortedList<em.StampTables, long>(); }
                if (DATA_STAMP_STORE.IndexOfKey(em.StampTables.dept_member_tb) == -1)
                {
                    DATA_STAMP_STORE.Add(em.StampTables.dept_member_tb, 0);
                    DATA_STAMP_STORE.Add(em.StampTables.dept_master_tb_ns,0);
                }
                if (DEPARTMENT_INNER_COLLECTION == null)
                {
                    DEPARTMENT_INNER_COLLECTION = new Dictionary<int, string>();
                    DEPARTMENT_INNER_COLLECTION.Add(-10, "Church Pastor");
                    DEPARTMENT_INNER_COLLECTION.Add(-11, "Church Elders");
                    DEPARTMENT_INNER_COLLECTION.Add(-12, "Family Life");
                    DEPARTMENT_INNER_COLLECTION.Add(-13, "Deaconary/Deaconesses");
                    DEPARTMENT_INNER_COLLECTION.Add(-14, "Treasurery");
                    DEPARTMENT_INNER_COLLECTION.Add(-15, "Children Ministry");
                    DEPARTMENT_INNER_COLLECTION.Add(-16, "Women Ministry");
                    DEPARTMENT_INNER_COLLECTION.Add(-17, "Adventist Men");
                    DEPARTMENT_INNER_COLLECTION.Add(-18, "Stewardship");
                    DEPARTMENT_INNER_COLLECTION.Add(-19, "Youth");
                    DEPARTMENT_INNER_COLLECTION.Add(-20, "Personal Ministry");
                    DEPARTMENT_INNER_COLLECTION.Add(-21, "Education");
                    DEPARTMENT_INNER_COLLECTION.Add(-22, "Health");
                    DEPARTMENT_INNER_COLLECTION.Add(-23, "Communication");
                    DEPARTMENT_INNER_COLLECTION.Add(-24, "Clerk/Secretary");
                    DEPARTMENT_INNER_COLLECTION.Add(-25, "Interest Co-ordination");
                    DEPARTMENT_INNER_COLLECTION.Add(-26, "Music");
                    DEPARTMENT_INNER_COLLECTION.Add(-27, "Public Affairs and Religious Liberty");
                    DEPARTMENT_INNER_COLLECTION.Add(-28, "Publishing");
                    DEPARTMENT_INNER_COLLECTION.Add(-29, "Development");
                    DEPARTMENT_INNER_COLLECTION.Add(-30, "Senior Citizens");
                    DEPARTMENT_INNER_COLLECTION.Add(-31, "Evaluation And Monitoring");
                    DEPARTMENT_INNER_COLLECTION.Add(-32, "Maintainance And Logistics");
                    DEPARTMENT_INNER_COLLECTION.Add(-33, "PathFinder Ministry");
                    DEPARTMENT_INNER_COLLECTION.Add(-34, "Welfare");
                    DEPARTMENT_INNER_COLLECTION.Add(-35, "Administration *");
                }
                string _table_name = "dept_master_tb_ns";
                if (DATA_DEPARTMENT == null)
                {
                    datam.DATA_DEPARTMENT = new SortedList<int, ic.departmentC>();
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
                var xd = _xd == null ? new xing() : _xd;
               
                    string _str = string.Empty;
                    var _stamp = xd.GetTimeStamp(_table_name);
                    if (DATA_DEPARTMENT.Keys.Count == 0)
                    {
                        _str = "select * from dept_master_tb_ns";
                        load_all = true;
                    }
                    else
                    {
                        if (wdata.TABLE_STAMP[_table_name] == _stamp)
                        {
                            return;
                        }
                        _str = string.Format("select * from dept_master_tb_ns where fs_time_stamp > {0}", wdata.TABLE_STAMP[_table_name]);
                    }
                    wdata.TABLE_STAMP[_table_name] = _stamp;
                    ic.departmentC _obj = null;
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
                                    _obj = new MTOMS.ic.departmentC();
                                    is_new = true;
                                }
                                else
                                {
                                    try
                                    {
                                        _obj = datam.DATA_DEPARTMENT[_dr["dept_id"].ToInt32()];
                                        is_new = false;
                                    }
                                    catch (Exception ex)
                                    {
                                        if (_obj == null)
                                        {
                                            _obj = new MTOMS.ic.departmentC();
                                            is_new = true;
                                        }
                                    }
                                }
                                if (is_new)
                                {
                                    _obj.dept_id = _dr["dept_id"].ToInt32();
                                    _obj.parent_id = _dr["parent_id"].ToInt32();
                                    datam.DATA_DEPARTMENT.Add(_obj.dept_id, _obj);
                                }
                                _obj.is_visible = _dr["is_visible"].ToByte() == 0 ? false : true;
                                _obj.index = _dr["s_index"].ToInt16();
                                _obj.level = _dr["s_level"].ToInt16();
                                _obj.dept_name = _dr["dept_name"].ToStringNullable();
                                _obj.expense_sys_account_id = _dr["sys_account_id"].ToInt32();
                                _obj.income_sys_account_id = _dr["inc_sys_account_id"].ToInt32();
                                _obj.cr_sys_account_id = _dr["cr_sys_account_id"].ToInt32();
                            }
                            _dr.Close(); _dr.Dispose();
                        }
                        #region update members
                        _table_name = "dept_member_tb";
                        _stamp = xd.GetTimeStamp(_table_name);
                        if (wdata.TABLE_STAMP.IndexOfKey(_table_name) == -1)
                        {
                            wdata.TABLE_STAMP.Add(_table_name, 0);
                        }
                        if (load_all)
                        {
                            _str = "select * from dept_member_tb where mem_status=1";
                        }
                        else
                        {
                            if (wdata.TABLE_STAMP[_table_name] == _stamp)
                            {
                                return;
                            }
                            _str = string.Format("select * from dept_member_tb where fs_time_stamp>{0}", wdata.TABLE_STAMP[_table_name]);
                        }
                        wdata.TABLE_STAMP[_table_name] = _stamp;
                        using (var _dr = xd.SelectCommand(_str))
                        {
                            while (_dr.Read())
                            {
                                try
                                {
                                    if (load_all)
                                    {
                                        datam.DATA_MEMBER[_dr["mem_id"].ToInt32()].DepartmentCollection.Add(datam.DATA_DEPARTMENT[_dr["dept_id"].ToInt32()]);
                                    }
                                    else
                                    {
                                        if (_dr["mem_status"].ToByte() == 0)
                                        {
                                            if (datam.DATA_MEMBER.Keys.IndexOf(_dr["mem_id"].ToInt32()) > -1)
                                            {
                                               if(datam.DATA_MEMBER[_dr["mem_id"].ToInt32()].DepartmentCollection.IndexOf(datam.DATA_DEPARTMENT[_dr["dept_id"].ToInt32()]) > -1)
                                                {
                                                    datam.DATA_MEMBER[_dr["mem_id"].ToInt32()].DepartmentCollection.Remove(datam.DATA_DEPARTMENT[_dr["dept_id"].ToInt32()]);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (datam.DATA_MEMBER[_dr["mem_id"].ToInt32()].DepartmentCollection.IndexOf(datam.DATA_DEPARTMENT[_dr["dept_id"].ToInt32()]) == -1)
                                            {
                                                datam.DATA_MEMBER[_dr["mem_id"].ToInt32()].DepartmentCollection.Add(datam.DATA_DEPARTMENT[_dr["dept_id"].ToInt32()]);
                                            }
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                    continue;
                                }
                            }
                            _dr.Close(); _dr.Dispose();
                        }
                        #endregion
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
                      
                    #endregion
                    if (_xd == null)
                    {
                        xd.CommitTransaction();
                        xd.Dispose();
                        xd = null;
                    }
                
            }
            private static void DepartmentInit(ref xing xd)
            {
                GetDepartments(xd);
            }
        }
    

}
