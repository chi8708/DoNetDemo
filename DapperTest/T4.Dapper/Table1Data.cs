
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由T4模板自动生成
//	   生成时间 2018-03-30 16:42:00
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失
//     作者：Harbour
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DapperTest.Model;

namespace DapperTest.Data
{	
	public partial class Table1Data
    { 
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Db_Table1 model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into Table1(");			
            strSql.Append("Id,name");
            strSql.Append(")  OUTPUT  inserted.Id values (");
            strSql.Append("@Id,@name");            
            strSql.Append(") ");

            return (int)DapperHelper.ExecuteScalar(strSql.ToString(), model);            
		}

        /// <summary>
        /// 添加多条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddBatch(List<Db_Table1> model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Table1(");
            strSql.Append("Id,name");
            strSql.Append(") values (");
            strSql.Append("@Id,@name");
            strSql.Append(") ");

            return DapperHelper.Excute(strSql.ToString(), model) > 0;       
        }
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Db_Table1 model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update Table1 set ");        
            strSql.Append(" Id = @Id , ");        
            strSql.Append(" name = @name  ");			
			strSql.Append(" where Id=@Id ");
            return DapperHelper.Excute(strSql.ToString(), model) > 0; 
		}

        public bool UpdateBatch(List<Db_Table1> model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Table1 set ");
            strSql.Append(" Id = @Id , ");
            strSql.Append(" name = @name  ");
            strSql.Append(" where Id=@Id ");
            return DapperHelper.Excute(strSql.ToString(), model) > 0;
        }
		
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete( int id)
		{	
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from Table1 ");
			strSql.Append(" where id=@id ");
			var newClass = new
			{
                Id=id
            };
			   
			return DapperHelper.Excute(strSql.ToString(), newClass) > 0; 
		}

        /// <summary>
        /// 删除多条数据
        /// </summary>
        public bool DeleteBatch(List<int> id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Table1 ");
            strSql.Append(" where id in @id ");
            var newClass = new
            {
                Id = id
            };

            return DapperHelper.Excute(strSql.ToString(), newClass) > 0;
        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Db_Table1 GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,name ");
			strSql.Append(" FROM Table1 ");
            strSql.Append(" where id=@Id ");
            var newClass = new
            {
                Id = id
            };
			return DapperHelper.FirstOrDefault<Db_Table1>(strSql.ToString(), newClass);
        }


		/// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Db_Table1> GetModelList(string strWhere, object param = null)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,name ");
			strSql.Append(" FROM Table1 ");
			if (strWhere.Trim() != "")
			{
			    strSql.Append(" where " + strWhere);
			}   
			return DapperHelper.Query<Db_Table1>(strSql.ToString(), param);
        }


		/// <summary>
		/// 获取总条数
		/// </summary>
		/// <param name="strWhere"></param>
		/// <returns></returns>
		public int GetDataRecord(string strWhere, object param = null)
		{
		    string sql = "select count(1) from Table1 where " + strWhere;
		    object obj = DapperHelper.ExecuteScalar(sql, param );
		    if (obj == null)
		    {
		        return 0;
		    }
		    return Convert.ToInt32(obj);
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public IEnumerable<Db_Table1> SelectListByPage(string strWhere, string orderby, int pageIndex, int pageSize,object param = null)
		{
		    StringBuilder strSql = new StringBuilder();
		    strSql.Append("SELECT * FROM ( ");
		    strSql.Append(" SELECT ROW_NUMBER() OVER (");
		    if (!string.IsNullOrEmpty(orderby.Trim()))
		    {
		        strSql.Append("order by T." + orderby);
		    }
		    else
		    {
		        strSql.Append("order by T.id desc");
		    }
		    strSql.Append(")AS Row, T.* from Table1 T ");
		    if (!string.IsNullOrEmpty(strWhere.Trim()))
		    {
		        strSql.Append(" WHERE   " + strWhere);
		    }
		    strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", (pageIndex-1)*pageSize+1,pageIndex * pageSize);

		    return DapperHelper.Query<Db_Table1>(strSql.ToString(), param);
		}

    
    }
}
