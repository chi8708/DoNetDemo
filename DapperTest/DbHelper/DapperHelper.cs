﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using System.Data;

namespace DapperTest
{
    public  class DapperHelper
    {
        private static readonly string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
       

        /// <summary>
        /// Excute返回受影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int Excute(string sql, object param, 
            IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                return conn.Execute(sql, param, transaction, commandTimeout, commandType);
            }
          
        }

        /// <summary>
        /// 查询返回list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<T> Query<T>(string sql, object param,
           IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                return conn.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType).ToList();
            }
        }

        /// <summary>
        /// 查询返回多个列表结果
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static SqlMapper.GridReader QueryMultiple(string sql, object param,
         IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                return conn.QueryMultiple(sql, param,transaction,commandTimeout,commandType);
            }
        }

        /// <summary>
        /// 返回受影响第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, object param,
           IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                return conn.ExecuteScalar(sql, param, transaction, commandTimeout, commandType);
            }
        }

        /// <summary>
        /// 符合条件的第一行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T FirstOrDefault<T>(string sql, object param) where T : class
        {
            return Query<T>(sql,param).FirstOrDefault();
        }


        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public static bool ExecTransaction(Dictionary<string,object> dic) 
        {
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in dic)
                        {
                            conn.Execute(item.Key, item.Value, transaction: transaction);
                        }
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

      
    }
}