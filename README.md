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

Still to implement
============
- CRC
- Response / re- Transmit
