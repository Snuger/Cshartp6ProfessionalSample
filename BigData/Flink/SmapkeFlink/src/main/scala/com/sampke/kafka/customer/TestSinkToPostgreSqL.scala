package com.sampke.kafka.customer

import java.sql.{Connection, DriverManager, PreparedStatement,Date}
import java.text.SimpleDateFormat

import org.apache.flink.configuration.Configuration
import org.apache.flink.streaming.api.functions.sink.{RichSinkFunction, SinkFunction}

class TestSinkToPostgreSqL extends RichSinkFunction[TestKafka] {
  private var connection: Connection = null
  private var ps: PreparedStatement = null
  //时间格式化
  val PRINT_DATEFORMAT: SimpleDateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss")

  override def open(parameters: Configuration): Unit = {
    super.open(parameters)
    val driver = "org.postgresql.Driver"
    val url = "jdbc:postgresql://172.19.30.241:5432/mcrptracedb"
    val username = "mcrp"
    val password = "mcrp"
    //1.加载驱动
    Class.forName(driver)
    //2.创建连接
    connection = DriverManager.getConnection(url, username, password)
    val sql = "insert into mcrp_dev.test01(id, text, updatetime)values(?,?,?);"
    //3.获得执行语句
    ps = connection.prepareStatement(sql)
  }

  //给参数赋值
  override def invoke(data: TestKafka, context: SinkFunction.Context): Unit = {
    try
      {
        ps.setString(1,data.id.toString())
        ps.setString(2,data.text)
        ps.setDate(3,new Date((data.updateTime.getTime())) )
        ps.execute()
      }catch {
         case e: Exception => println(e.getMessage)
    }
  }
  //5.关闭连接和释放资源
  override def close(): Unit = {
    if (connection != null) {
      connection.close()
    }
    if (ps != null) {
      ps.close()
    }
    super.close()
  }
}
