using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPatternDesigin
{
    public class V1cPlayer : AdvanceMediaPlayer
    {
      public string Play(string name) => $"正在使用{nameof(V1cPlayer)},播放视频文件{name}";
    }
}
