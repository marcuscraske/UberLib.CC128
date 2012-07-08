﻿/*
 * UBERMEAT FOSS
 * ****************************************************************************************
 * License:                 Creative Commons Attribution-ShareAlike 3.0 unported
 *                          http://creativecommons.org/licenses/by-sa/3.0/
 * 
 * Project:                 UberLib.CC128
 * File:                    /Main.cs
 * Author(s):               limpygnome						limpygnome@gmail.com
 * To-do/bugs:              none
 * 
 * Responsible for demonstrating and testing the library with the CC128.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;
using System.Xml;
using UberLib.CC128;

namespace CC128_Test
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            EnergyMonitor mon = new EnergyMonitor();
            mon.eventNewSensorData += new EnergyMonitor._eventNewSensorData(mon_eventNewSensorData);
            mon.eventNewSensorDataMalformed += new EnergyMonitor._eventNewSensorDataMalformed(mon_eventNewSensorDataMalformed);
            mon.eventStateChange += new EnergyMonitor._eventStateChange(mon_eventStateChange);
            mon.start();
        }
        void mon_eventStateChange(EnergyMonitor.State newState)
        {
            textbox("New state: " + newState.ToString() + "\r\n");
        }
        void mon_eventNewSensorDataMalformed(string data, Exception ex)
        {
            textbox("Malformed data - " + ex.Message + "\r\n");
        }
        void mon_eventNewSensorData(EnergyReading reading)
        {
            textbox(reading[0] + " watts - " + reading.CurrentTime + " monitor time - " + reading.Temperature + " celsius - " + reading.Software + "\r\n");
        }
        void textbox(string data)
        {
            textBox1.Invoke((MethodInvoker)delegate()
            {
                textBox1.Text += DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss: ") + data;
                textBox1.SelectionStart = textBox1.TextLength;
                textBox1.ScrollToCaret();
            });
        }
        private void buttClear_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }
        private void buttExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}