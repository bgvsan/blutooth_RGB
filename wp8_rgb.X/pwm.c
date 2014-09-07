#include <xc.h>

void init_pwm() {

    //Change pin out pwm on RA4 - RA5
    APFCONbits.P1SEL = 1;
    APFCONbits.P2SEL = 1;

    PWM1CONbits.EN = 1;
    PWM1CONbits.OE = 1;
    PWM1CONbits.MODE = 00;
    PWM1CONbits.POL = 0;

    PWM2CONbits.EN = 1;
    PWM2CONbits.OE = 1;
    PWM2CONbits.MODE = 00;
    PWM2CONbits.POL = 0;

    PWM3CONbits.EN = 1;
    PWM3CONbits.OE = 1;
    PWM3CONbits.MODE = 00;
    PWM3CONbits.POL = 0;

    ///PWM CLOCK

    PWM1CLKCONbits.PS = 00;
    PWM1CLKCONbits.CS = 00;


    PWM2CLKCONbits.PS = 00;
    PWM2CLKCONbits.CS = 00;

    PWM3CLKCONbits.PS = 00;
    PWM3CLKCONbits.CS = 00;


    //PWM SET PERIOD

    PWM1PRH = 0xff;
    PWM1PRL = 0xff;
    PWM1DCH = 0xff;
    PWM1DCL = 0xff;

    PWM2PRH = 0xff;
    PWM2PRL = 0xff;
    PWM2DCH = 0xff;
    PWM2DCL = 0xff;

    PWM3PRH = 0xff;
    PWM3PRL = 0xff;
    PWM3DCH = 0xff;
    PWM3DCL = 0xff;


}

void close_PWM() {
    PWM1CONbits.OE = 0;
    PWM2CONbits.OE = 0;
    PWM3CONbits.OE = 0;
}

void open_PWM() {
    PWM1CONbits.OE = 1;
    PWM2CONbits.OE = 1;
    PWM3CONbits.OE = 1;
}

void set_pwmRED(unsigned int value) {
    PWM1PHH = (unsigned char) (value >> 8);
    PWM1PHL = (unsigned char) (value);
    //reset pwm
    PWM1LD = 0xFF;
}

void set_pwmGREEN(unsigned int value) {
    PWM2PHH = (unsigned char) (value >> 8);
    PWM2PHL = (unsigned char) (value);
    //reset pwm
    PWM2LD = 0xFF;

}

void set_pwmBLUE(unsigned int value) {

    PWM3PHH = (unsigned char) (value >> 8);
    PWM3PHL = (unsigned char) (value);
    //reset pwm
    PWM3LD = 0xFF;
}

void set_color(unsigned int color[]) {

    set_pwmRED(color[1]);
    set_pwmGREEN(color[2]);
    set_pwmBLUE(color[3]);

    color[0] = 0;
    color[1] = 0;
    color[2] = 0;
    color[3] = 0;
    color[4] = 0;
}
