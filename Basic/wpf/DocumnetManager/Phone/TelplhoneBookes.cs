using System;
using System.Collections.Generic;
using System.Text;

namespace DocumnetManager.Phone
{
    public class TelplhoneBookes<TPhoneNumber> where TPhoneNumber : PhoneNumber
    {
        TPhoneNumber[] phones;
        int end;

        public TelplhoneBookes()
        {
            phones = new TPhoneNumber[10];
            end = 0;
        }

        /// <summary>
        /// 添加电话号到电话本
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public bool Add(TPhoneNumber phoneNumber) {
            if (end >= 10)
                return false;
            phones[end] = phoneNumber;
            end++;
            return true;
        }

        /// <summary>
        /// 打印所有的电话号吗
        /// </summary>
        public void DisplayAllPhone() {
            foreach (TPhoneNumber number in phones) {
                if (number != null)
                    Console.WriteLine($"联系人:{number.Name},电话号码:{number.Number}");
            }
        }


        public TPhoneNumber GetPhoneByName(string name) {
            TPhoneNumber phone = default(TPhoneNumber);
            foreach (TPhoneNumber number in phones) {
                if (number.Name == name)
                {
                    phone = number;
                    break;
                }                  
            }
            return phone;
        }


    }
}
