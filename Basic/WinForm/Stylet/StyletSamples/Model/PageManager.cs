﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StyletSamples.View;

namespace StyletSamples.ViewModel
{
    public class PageManager
    {
        public static WelcomeView PageWellcome = new WelcomeView();

        public static BorderSample BorderSamples = new BorderSample();

        public static TextBoxSample TextBoxSamples = new TextBoxSample();

        public static ListBoxSampleView ListBoxSample = new ListBoxSampleView();

        public static GradUseSample GradUseSample = new GradUseSample();

        public static ButtonSample ButtonSample = new ButtonSample();

    }
}