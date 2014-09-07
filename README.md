Bluetooth RGB Module
============

This is an HC-06 bluetooth / pic module for drive an rgb led throught your modile phone

Protocol
============
Read from serial port the Red/Green/Blue value a and set the pwm output to show the correct color
Package send
{0x01,HEXRed,HEXGreen;HEXBlue,0xFF}

Implemented
============
- Board and gerber file (powered by USB 5V Tollerant)
- Comunication protocol
- Bluetooth library
- WP8 Application

Module Pic12f1572
============

![Hardware module Front](//images/WP_20140907_005.jpg)
![Hardware module Back](//images/WP_20140907_006.jpg)
Video
============

<a href="http://www.youtube.com/watch?feature=player_embedded&v=Vz8ssNsL01g
" target="_blank"><img src="https://img.youtube.com/vi/Vz8ssNsL01g/0.jpg" 
alt="WP8 Bluetooth RGB" width="320" height="240" border="10" /></a>

Still to implement
============
- CRC
- Response / re-Transmit ACK