package com.sampke.Temperature.customer.customer

import com.alibaba.fastjson.JSON

import java.text.SimpleDateFormat
import java.util.{Date, Properties, TimeZone, UUID}
import org.apache.flink.api.common.functions.MapFunction
import org.apache.flink.api.common.typeinfo.TypeInformation
import org.apache.flink.streaming.api.scala.{StreamExecutionEnvironment, createTuple2TypeInformation, createTypeInformation}
import org.apache.flink.streaming.connectors.kafka.{FlinkKafkaConsumer, KafkaDeserializationSchema}
import org.apache.kafka.clients.consumer.ConsumerRecord
import org.apache.kafka.common.serialization.StringSerializer


//读取kafka中数据  key value全部读出来
object TemperatureCustomer {
  def main(args: Array[String]): Unit = {
    val env = StreamExecutionEnvironment.getExecutionEnvironment
    // kafka 配置
    val KAFKA_BROKERS = "172.26.57.95:9092"
    val TOPIC_NAME = "iot_platform_tiwentie"
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
      override def map(t: (String, String)): String = "读取一条消息------->|"+t._2+"|----------->"+ PRINT_DATEFORMAT.format(new Date())
    })
    //转换后进行输出
    readText.print()

    var dataTransOUt=stream.map((new MapFunction[(String,String),TemperatureMessage] {
      override def map(t: (String, String)): TemperatureMessage = TemperatureMessage(UUID.randomUUID(),t._2,new Date())
    }))
    dataTransOUt.addSink(new TemperatureToPostgreSqL).name("体温消费")

//    var dataTransOut1=stream.map(new MapFunction[(String,String),TemperatureStorage] {
//      override def map(t: (String, String)): TemperatureStorage = messageAnalyse(t._2)
//    }).filter(item=>item.tiWen>38)
//    dataTransOut1.print()

    env.execute("体温监测")
  }
  def messageAnalyse(mesage:String):TemperatureStorage={
    val temperatureData:TemperatureStorage=JSON.parseObject(mesage, classOf[TemperatureStorage])
    return TemperatureStorage(temperatureData.sheBeiID,temperatureData.tiWen,temperatureData.caiJiSJ)
  }

}
