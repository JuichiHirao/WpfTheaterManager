using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTheaterManager.common;
using WpfTheaterManager.data;

namespace WpfTheaterManager.service
{
    class Akb48Service
    {

        public List<Akb48Data> GetList(MySqlDbConnection myDbCon)
        {
            List<Akb48Data> listData = new List<Akb48Data>();

            MySqlDbConnection dbcon;
            string sqlcmd = "";

            // 引数にコネクションが指定されていた場合は指定されたコネクションを使用
            if (myDbCon != null)
                dbcon = myDbCon;
            else
                dbcon = new MySqlDbConnection();

            sqlcmd = "SELECT id "
                + ", group_name, title, subtitle, member "
                + ", the_date, memo, memo2, remark "
                + ", filename, rating1, rating2, status "
                + ", created_at, updated_at "
                + "FROM tv.akb48 "
                // + "WHERE member is not null and length(member) > 0 "
                + "ORDER BY created_at DESC ";

            MySqlDataReader reader = null;
            try
            {
                reader = dbcon.GetExecuteReader(sqlcmd);

                do
                {
                    if (reader.IsClosed)
                    {
                        //_logger.Debug("reader.IsClosed");
                        throw new Exception("AKB48の取得でreaderがクローズされています");
                    }

                    while (reader.Read())
                    {
                        Akb48Data data = new Akb48Data();

                        data.Id = MySqlExportCommon.GetDbInt(reader, 0);
                        data.GroupName = MySqlExportCommon.GetDbString(reader, 1);
                        data.Title = MySqlExportCommon.GetDbString(reader, 2);
                        data.SubTitle = MySqlExportCommon.GetDbString(reader, 3);
                        data.Member = MySqlExportCommon.GetDbString(reader, 4);
                        data.TheDate = MySqlExportCommon.GetDbDateTime(reader, 5);
                        data.Memo = MySqlExportCommon.GetDbString(reader, 6);
                        data.Memo2 = MySqlExportCommon.GetDbString(reader, 7);
                        data.Remark = MySqlExportCommon.GetDbString(reader, 8);
                        data.Filename = MySqlExportCommon.GetDbString(reader, 9);
                        data.Rating1 = MySqlExportCommon.GetDbInt(reader, 10);
                        data.Rating2 = MySqlExportCommon.GetDbInt(reader, 11);
                        data.Status = MySqlExportCommon.GetDbString(reader, 12);
                        data.CreatedAt = MySqlExportCommon.GetDbDateTime(reader, 13);
                        data.UpdatedAt = MySqlExportCommon.GetDbDateTime(reader, 14);

                        listData.Add(data);
                    }
                } while (reader.NextResult());
            }
            finally
            {
                reader.Close();
            }

            myDbCon.closeConnection();

            return listData;
        }

        public void Update(Akb48Data myData)
        {
            MySqlDbConnection dbcon = new MySqlDbConnection();
            string sqlcmd = "UPDATE tv.akb48 "
                + "SET "
                + "group_name = @pGroupName"
                + ", title = @pTitle "
                + ", subtitle = @pSubTitle "
                + ", member = @pMember "
                + ", the_date = @pTheDate "
                + ", memo = @pMemo"
                + ", memo2 = @pMemo2 "
                + ", filename = @pFilename "
                + ", rating1 = @pRating1 "
                + ", rating2 = @pRating2 "
                + ", status = @pStatus "
                + "WHERE id = @pId ";

            List<MySqlParameter> sqlparamList = new List<MySqlParameter>();

            MySqlParameter param = new MySqlParameter();

            param = new MySqlParameter("@pGroupName", MySqlDbType.VarChar);
            param.Value = myData.GroupName;
            sqlparamList.Add(param);

            param = new MySqlParameter("@pTitle", MySqlDbType.VarChar);
            param.Value = myData.Title;
            sqlparamList.Add(param);

            param = new MySqlParameter("@pSubTitle", MySqlDbType.VarChar);
            param.Value = myData.SubTitle;
            sqlparamList.Add(param);

            param = new MySqlParameter("@pMember", MySqlDbType.VarChar);
            param.Value = myData.Member;
            sqlparamList.Add(param);

            param = new MySqlParameter("@pTheDate", MySqlDbType.DateTime);
            param.Value = myData.TheDate;
            sqlparamList.Add(param);

            param = new MySqlParameter("@pMemo", MySqlDbType.VarChar);
            param.Value = myData.Memo;
            sqlparamList.Add(param);

            param = new MySqlParameter("@pMemo2", MySqlDbType.VarChar);
            param.Value = myData.Memo2;
            sqlparamList.Add(param);

            param = new MySqlParameter("@pFilename", MySqlDbType.VarChar);
            param.Value = myData.Filename;
            sqlparamList.Add(param);

            param = new MySqlParameter("@pRating1", MySqlDbType.Int16);
            param.Value = myData.Rating1;
            sqlparamList.Add(param);

            param = new MySqlParameter("@pRating2", MySqlDbType.Int16);
            param.Value = myData.Rating2;
            sqlparamList.Add(param);

            param = new MySqlParameter("@pStatus", MySqlDbType.VarChar);
            param.Value = myData.Status;
            sqlparamList.Add(param);

            param = new MySqlParameter("@pId", MySqlDbType.Int32);
            param.Value = myData.Id;
            sqlparamList.Add(param);

            dbcon.SetParameter(sqlparamList.ToArray());
            dbcon.execSqlCommand(sqlcmd);

        }
    }
}
