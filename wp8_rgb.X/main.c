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

// initialiaze peripherals
void init(void);
void interrupt isr(void);
//void configUSART(void);


void main() {

    while(1)
    {
        __delay_ms(100);
    }


}

// initialiaze peripherals
void init()
    {
    //CLOCK 16MHz
    OSCCON = 0b01111010;
    
    //PIN CONFIG
    TRISA = 0x00;
    PORTA = 0x01; //RX input

    //EUART init
    configUSART();
    enableUSART_RX();

    //INTERRUPT ENABLE
    PIE1bits.RCIE = 1;
    INTCONbits.PEIE = 1;
    INTCONbits.GIE = 1;
    //PWM INIT

    }


void interrupt isr(void)
{
    //interrupt SERIAL PORT RECEIVED
    if (RCIF == 1)
    {
       // read uart to clear interrupt flag
        readUSART();
        if(OERR ==1)
        {
            CREN = 0;//Clear error flag
        }
    }

}


