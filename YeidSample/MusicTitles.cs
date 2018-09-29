using System;
using System.Collections.Generic;
using System.Text;

namespace YeidSample
{
    public class MusicTitles
    {
        string[] musics = {"你是我的月亮","你是风儿我是沙","九百九十九朵玫瑰" };

        public IEnumerator<string> GetEnumerator() {

            foreach (string music in musics)
            {
                yield return music;
            }

        }

        public IEnumerable<string> Reverse() {

            for (int t = 2; t > 0; t--)
            {
                yield return musics[t];
            }
        }

        public IEnumerable<string> Substr(int index, int length) {
            for (int i = 0; i < 2; i++) {
                yield return musics[i].Substring(index, length);

            }
        }

    }
}
