package com.sampke.dataset
import org.apache.flink.streaming.api.scala._
import com.sampke.dataset.model._


import org.apache.flink.api.common.functions.MapFunction

object DataSetSmple {

  def main(args: Array[String]): Unit = {
    val env = StreamExecutionEnvironment.getExecutionEnvironment

     val streamInput:DataStream[Student]= env.fromElements(
        Student("张三",10,102.3),
        Student("李四",22,112.3),
         Student("王五",32,122.3),
        Student("赵六",33,132.3)
     )



   //val streamOutPut=env.fromDataStream(streamInput)


  }
}
