package com.msb.stream.source

import java.sql.{Connection, DriverManager, Statement}
import java.text.SimpleDateFormat
import java.util.{Date, Properties, UUID}

import com.sampke.kafka.customer.{TestKafka, TestSinkToPostgreSqL}
import org.apache.flink.api.common.functions.MapFunction
import org.apache.flink.api.common.typeinfo.TypeInformation
import org.apache.flink.streaming.api.scala.{StreamExecutionEnvironment, createTuple2TypeInformation, createTypeInformation}
import org.apache.flink.streaming.connectors.kafka.{FlinkKafkaConsumer, KafkaDeserializationSchema}
import org.apache.kafka.clients.consumer.ConsumerRecord
import org.apache.kafka.common.serialization.StringSerializer
import org.apache.flink.streaming.api.scala._




//读取kafka中数据  key value全部读出来
object YongHuXXCustomer {
  def main(args: Array[String]): Unit = {
    val env = StreamExecutionEnvironment.getExecutionEnvironment

    // kafka 配置
    val KAFKA_BROKERS = "172.19.32.51:9092,172.19.32.52:9092,172.19.32.53:9092,172.19.32.54:9092,172.19.32.55:9092"
    val TOPIC_NAME = "mcrp-fink-test"
    //时间格式化
    val PRINT_DATEFORMAT: SimpleDateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss")

    //设置连接kafka的配置信息
    val props = new Properties()
    props.setProperty("bootstrap.servers", KAFKA_BROKERS)
    props.setProperty("group.id",TOPIC_NAME)
      props.setProperty("key.deserializer",classOf[StringSerializer].getName)
    props.setProperty("value.deserializer",classOf[StringSerializer].getName)

    //第一个参数 ： 消费的topic名
    val stream = env.addSource(new FlinkKafkaConsumer[(String, String)](TOPIC_NAME, new KafkaDeserializationSchema[(String, String)] {
      //什么时候停止，停止条件是什么
      override def isEndOfStream(t: (String, String)): Boolean = false

      //要进行序列化的字节流
      override def deserialize(consumerRecord: ConsumerRecord[Array[Byte], Array[Byte]]): (String, String) = {
        val key = new String(consumerRecord.key(), "UTF-8")
        val value = new String(consumerRecord.value(), "UTF-8")
        (key, value)
      }
      //指定一下返回的数据类型  Flink提供的类型
      override def getProducedType: TypeInformation[(String, String)] = {
        createTuple2TypeInformation(createTypeInformation[String], createTypeInformation[String])
      }
    }, props))

    //数据转换（将DataStram中的数组转换成想要的字符串）
   val readText=  stream.map(new MapFunction[(String,String),String] {
      override def map(t: (String, String)): String = "读取一条消息------->|"+t._2.toUpperCase()+"|----------->"+ PRINT_DATEFORMAT.format(new Date())
    })
    //转换后进行输出
    readText.print()

    var dataTransOUt=stream.map((new MapFunction[(String,String),TestKafka] {
      override def map(t: (String, String)): TestKafka = TestKafka(UUID.randomUUID(),t._2,new Date())
    }))
   dataTransOUt.addSink(new TestSinkToPostgreSqL)


    env.execute("kafka customer")
  }
}
