#include <xc.h>
#include "pwm.h"
///se the controller to receive data from rx pin
unsigned char cUART_char;
unsigned char cUART_data_count = 0;
unsigned int cUART_data_message[5];

void init_uart(void) // init UART module for 9600bps boud, start bit 1, stopbit 1, parity NONE
{

    TRISAbits.TRISA1=1; //Make UART RX pin input
    TRISAbits.TRISA0=0; //Make UART TX pin output
    SPBRGH = 0x01; //9600bps 16MHz Osc
    SPBRGL = 0xA0;

    RCSTAbits.CREN=1; //1 = Enables receiver
    RCSTAbits.SPEN=1; //1 = Serial port enabled (configures RX/DT and TX/CK pins as serial port pins)
    BAUDCONbits.BRG16=1;//1 = 16-bit Baud Rate Generator ? SPBRGH and SPBRGL

    TXSTAbits.SYNC=0; //0 = Asynchronous mode
    TXSTAbits.BRGH=1; //1 = High speed
    TXSTAbits.TXEN=1; //1 = Transmit enabled
    PIE1bits.RCIE=1; // 1 = Enables the EUSART receive interrupt
    INTCONbits.GIE = 1;//enable interrupts
    INTCONbits.PEIE = 1;


}
void putch(unsigned char byte) {
    /* output one byte */
    while(!TXIF)	/* set when register is empty */
            continue;
    TXREG = byte;
}

void get_message()
{
 
    cUART_char = RCREGbits.RCREG; // read new data into variable

    if(cUART_data_count < 4)
    {
        cUART_data_message[cUART_data_count] = (int)((cUART_char)*257);
        cUART_data_count++;
    }
    else
    {
        set_color(cUART_data_message);
        cUART_data_count = 0;
    }
    
}

