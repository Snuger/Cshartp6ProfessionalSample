package com.sampke.collections.customer


case class WC(val word:String,val salary:Int) {
  var Word=word
  var Salary=salary

  override def toString: String = "{word:"+Word+",salary"+Salary+"}"
}
