package com.sampke.kafka.customer
import org.apache.flink.streaming.api.TimeCharacteristic
import org.apache.kafka.clients._
import org.apache.flink.streaming.api.scala._
import org.apache.flink.streaming.connectors.kafka._
import java.util.Properties




object YongHuXXCustomer {
  def main(args: Array[String]): Unit = {
    val env=StreamExecutionEnvironment.getExecutionEnvironment

    env.enableCheckpointing(5000);
    env.setStreamTimeCharacteristic(TimeCharacteristic.EventTime);

    val properties = new Properties()
    properties.setProperty("bootstrap.servers", "10.47.85.158:9092")
    properties.setProperty("group.id", "flink-group")


  }
}
