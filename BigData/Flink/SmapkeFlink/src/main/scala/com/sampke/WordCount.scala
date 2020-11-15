package com.sampke

import  org.apache.flink.streaming.api.scala._

object WordCount {
  def main(args: Array[String]): Unit = {
     val env=StreamExecutionEnvironment.getExecutionEnvironment
     var intputDataStream=env.socketTextStream("127.0.0.1",5650)
     var resultStream= intputDataStream.flatMap(_.split(" "))
        .filter(_.nonEmpty)
        .map((_,1))
        .keyBy(0)
        .sum(1)




  }
}
