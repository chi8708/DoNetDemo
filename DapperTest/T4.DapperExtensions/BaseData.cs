﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using DapperExtensions;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace DapperTest.Data
{
    public partial class BaseData<T> where T : class ,new()
    {

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public dynamic Insert(T model)
        {
            dynamic r = null;
            using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
            {
                cn.Open();
                r = cn.Insert(model);
                cn.Close();
            }

            return r;
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public bool InsertBatch(List<T> models)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
                {
                    cn.Open();
                    foreach (var model in models)
                    {
                        cn.Insert(model);
                    }
                    cn.Close();
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }



        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(T model)
        {
            dynamic r = null;
            using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
            {
                cn.Open();
                r = cn.Update(model);
                cn.Close();
            }

            return r;
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public bool UpdateBatch(List<T> models)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
                {
                    cn.Open();
                    foreach (var model in models)
                    {
                        cn.Update(model);
                    }
                    cn.Close();
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }


        /// <summary>
        ///根据实体删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public dynamic Delete(T model)
        {
            dynamic r = null;
            using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
            {
                cn.Open();
                r = cn.Delete(model);
                cn.Close();
            }

            return r;
        }


        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public dynamic Delete(object predicate)
        {
            dynamic r = null;
            using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
            {
                cn.Open();
                r = cn.Delete(predicate);
                cn.Close();
            }

            return r;
        }

        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public bool DeleteBatch(List<T> models)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
                {
                    cn.Open();
                    foreach (var model in models)
                    {
                        cn.Delete(model);
                    }
                    cn.Close();
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }


        /// <summary>
        /// 根据一个实体对象
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public T Get(object id)
        {
            T t = default(T);
            using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
            {
                cn.Open();
                t = cn.Get<T>(id);
                cn.Close();
            }

            return t;

        }


        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public List<T> GetList(object predicate = null, IList<ISort> sort = null)
        {
            List<T> t = null;
            using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
            {
                cn.Open();
                t = cn.GetList<T>(predicate, sort).ToList();//不使用ToList  SqlConnection未初始化
                cn.Close();
            }

            return t;

        }

        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public List<T> GetList(string where, string sort = null)
        {
            var tableName = typeof(T).Name;
            StringBuilder sql = new StringBuilder().AppendFormat("SELECT * FROM {0} ", tableName);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where {0} ", where);
            }
            if (!string.IsNullOrEmpty(sort))
            {
                sql.AppendFormat(" order by {0} ", sort);
            }

            using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
            {
                return cn.Query<T>(sql.ToString()).ToList();
            }

        }


        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public int Count(object predicate = null)
        {
            int t = 0;
            using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
            {
                cn.Open();
                t = cn.Count<T>(predicate);
                cn.Close();
            }

            return t;
        }


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="sort">排序</param>
        /// <param name="page">页索引</param>
        /// <param name="resultsPerPage">页大小</param>
        /// <returns></returns>
        public List<T> GetPage(object predicate, IList<ISort> sort, int page, int resultsPerPage)
        {
            List<T> t = null;
            using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
            {
                cn.Open();
                t = cn.GetPage<T>(predicate, sort, page, resultsPerPage).ToList();
                cn.Close();
            }

            return t;

        }


        /// <summary>
        /// 存储过程分页查询
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="sort">分类</param>
        /// <param name="page">页索引</param>
        /// <param name="resultsPerPage">页大小</param>
        /// <param name="fields">查询字段</param>
        /// <returns></returns>
        public PageDateRep<T> GetPage(string where, string sort, int page, int resultsPerPage, string fields = "*")
        {
            var tableName = typeof(T).Name;
            var p = new DynamicParameters();
            p.Add("@TableName", tableName);
            p.Add("@Fields", fields);
            p.Add("@OrderField", sort);
            p.Add("@sqlWhere", where);
            p.Add("@pageSize", resultsPerPage);
            p.Add("@pageIndex", page);
            p.Add("@TotalPage", 0, direction: ParameterDirection.Output);
            p.Add("@Totalrow", 0, direction: ParameterDirection.Output);

            using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
            {

                var data = cn.Query<T>("P_ZGrid_PagingLarge", p, commandType: CommandType.StoredProcedure);
                int totalPage = p.Get<int>("@TotalPage");
                int totalrow = p.Get<int>("@Totalrow");

                var rep = new PageDateRep<T>()
                {
                    code = 0,
                    count = totalrow,
                    data = data.ToList()
                };

                return rep;
            }
        }
    }
}