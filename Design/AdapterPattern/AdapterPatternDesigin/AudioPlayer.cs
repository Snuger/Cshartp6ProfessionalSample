using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPatternDesigin
{
    public class AudioPlayer
    {
        private MediaAdapter _adaper;

        public string Play(string audioType, string name)
        {
            _adaper = new MediaAdapter(audioType);
            return _adaper.Play(name);           
        }
    }
}
