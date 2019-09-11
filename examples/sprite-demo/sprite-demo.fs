title: SPRITE-DEMO
makercode: TK

require gbhw.fs
require input.fs
require lcd.fs
require ibm-font.fs
require struct.fs
( program start )

:m new CREATE allot ;
:m sprite 4 * _OAM + CONSTANT ;

ROM

struct
  1 chars field spr-y
  1 chars field spr-x
  1 chars field spr-tile
  1 chars field spr-opts
end-struct sprite%

0 SPRITE player

: v! lcd-wait-vblank c! ;

: playerX+! player spr-x lcd-wait-vblank +! ;
: playerY+! player spr-y lcd-wait-vblank +! ;

: handle-input
  begin
    key-state
    dup k-right and if  1 playerX+! then
    dup k-left  and if -1 playerX+! then
    dup k-up    and if -1 playerY+! then
    dup k-down  and if  1 playerY+! then
    dup k-a     and if  1 player spr-tile v! then
    dup k-b     and if  2 player spr-tile v! then
    \ If there no key pressed, wait for one
    0= if halt then
  again ;

: enable-sprites
  [ LCDCF_ON
    LCDCF_BG8000 or
    LCDCF_BGON or
    LCDCF_OBJ8 or
    LCDCF_OBJON or ]L
  rLCDC c! ;

: clear-background
  _SCRN0 [ SCRN_VX_B SCRN_VY_B * ]L blank ;

: clear-sprites
  _OAM [ 40 4 * ]L erase ;

: init-player-sprite
  #1        player spr-tile c!
  %00000000 player spr-opts c!
  #80       player spr-y    c!
  #50       player spr-x    c! ;

: main
  init-input
  install-font
  disable-lcd
  clear-background
  clear-sprites
  init-player-sprite
  enable-sprites
  handle-input ;
