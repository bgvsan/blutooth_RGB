#include <xc.h>
///se the controller to receive data from rx pin



void enableUSART_RX()
{
    SPEN = 1;//Enable serial
    CREN = 1;
    SYNC = 0;
    SPEN = 1;
}

void configUSART()
{
    BRG16 = 0;
    BRGH = 1;

    //value of boud rate = 9615

    SPBRGH = 0x96;
    SPBRGL = 0x15;
}

char readUSART()
{
    return RCREG;
}