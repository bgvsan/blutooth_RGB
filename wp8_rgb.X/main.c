/* 
 * File:   main.c
 * Author: diego
 *
 * Created on 5 giugno 2014, 22.46
 *
 * Firmware for bluetooth module usart RGB
 *
 * Protocol to receive and response message
 * FRAME:
 * START MESSAGE    0x01
 * RED COLOR        0XXX
 * GREEN COLOR      0XXX
 * BLUE COLOR       0XXX
 * END MESSAGE      0XFF
 *
 * RESPOSNSE RECEIVED MESSAGE 0X01
 */

#include <xc.h>
#include "usart.h"
#include "pwm.h"


#define _XTAL_FREQ 16000000 //The speed of your internal(or)external oscillator

// #pragma config statements should precede project file includes.
// Use project enums instead of #define for ON and OFF.

// CONFIG1
#pragma config FOSC = INTOSC    //  (INTOSC oscillator; I/O function on CLKIN pin)
#pragma config WDTE = OFF       // Watchdog Timer Enable (WDT disabled)
#pragma config PWRTE = OFF      // Power-up Timer Enable (PWRT disabled)
#pragma config MCLRE = OFF      // MCLR Pin Function Select (MCLR/VPP pin function is digital input)
#pragma config CP = OFF         // Flash Program Memory Code Protection (Program memory code protection is disabled)
#pragma config BOREN = OFF      // Brown-out Reset Enable (Brown-out Reset disabled)
#pragma config CLKOUTEN = OFF   // Clock Out Enable (CLKOUT function is disabled. I/O or oscillator function on the CLKOUT pin)

// CONFIG2
#pragma config WRT = OFF        // Flash Memory Self-Write Protection (Write protection off)
#pragma config PLLEN = OFF      // PLL Enable (4x PLL disabled)
#pragma config STVREN = ON      // Stack Overflow/Underflow Reset Enable (Stack Overflow or Underflow will cause a Reset)
#pragma config BORV = LO        // Brown-out Reset Voltage Selection (Brown-out Reset Voltage (Vbor), low trip point selected.)
#pragma config LPBOREN = OFF    // Low Power Brown-out Reset enable bit (LPBOR is disabled)
#pragma config LVP = ON         // Low-Voltage Programming Enable (Low-voltage programming enabled)

#define LEDGREEN   LATAbits.LATA4
#define LEDBLUE  LATAbits.LATA2
#define LEDRED LATAbits.LATA5


void init_uart(void);
void init(void);
void interrupt InterruptHandlerLow(void);

unsigned char cUART_char;

void main() {

    init();
    init_uart();
    LATA = 0xff;

    init_pwm();
    close_PWM();
    open_PWM();

    LATA = 0xff;

    set_pwmBLUE(0);
    set_pwmGREEN(0);
    set_pwmRED(0);
    while (1)
    {
        
    }
}

// initialiaze peripheral
void init() {
    //CLOCK 16MHz
    OSCCON = 0b01111010;
     //PIN CONFIG
    TRISA = 0b000000;
    ANSELA = 0X00;
    LATA = 0XFF;
}

void interrupt InterruptHandlerLow ()
{
    //interrupt SERIAL PORT RECEIVED
    if (PIR1bits.RCIF == 1)
    {
        // read uart to clear interrupt flag
        if (RCSTAbits.OERR == 1) {
            RCSTAbits.CREN = 0; //Clear error flag
            cUART_char = RCREGbits.RCREG ;
            RCSTAbits.CREN = 1;
        }

        else
        {
            get_message();
        }
    }

}


