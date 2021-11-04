using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPatternDesigin
{
    public class MediaAdapter : MediaPlayer
    {
        private  AdvanceMediaPlayer _player;

        public MediaAdapter(string audioType)
        {           
            if (audioType.Equals("v1c", StringComparison.OrdinalIgnoreCase))
                _player = new  V1cPlayer();
            if (audioType.Equals("mp4", StringComparison.OrdinalIgnoreCase))
                _player = new Mp4Player();
        }        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public string Play(string name)=> _player.Play(name);
    }
}
