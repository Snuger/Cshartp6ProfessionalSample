using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPatternDesigin
{
    public class Mp4Player : AdvanceMediaPlayer
    {
        public string Play(string name) => $"正在使用{nameof(Mp4Player)},播放视频文件{name}";
    }
}
