[asm]

main:

ps-init,

$0104 ps-push-lit,
' @ # call,
$0106 ps-push-lit,
' @ # call,

label loop
halt,
loop jr,

[endasm]
