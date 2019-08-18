title: EXAMPLE
gamecode: HELO
makercode: RX

require gbhw.fs
require input.fs
require lcd.fs
require ibm-font.fs

( program start )

_OAM 0 + constant player.y
_OAM 1 + constant player.x
_OAM 2 + constant player.tile
_OAM 3 + constant player.opts

: playerX+! player.x lcd-wait-vblank +! ;
: playerY+! player.y lcd-wait-vblank +! ;

: handle-input
  begin
    rDIV c@ [ $FF 8 / ]L < if
      key-state
      dup k-right and if  1 playerX+! then
      dup k-left  and if -1 playerX+! then
      dup k-up    and if -1 playerY+! then
      dup k-down  and if  1 playerY+! then
      dup k-a     and if  1 player.tile lcd-wait-vblank c! then
      dup k-b     and if  2 player.tile lcd-wait-vblank c! then
      \ If there no key pressed, wait for one
      0= if halt then
    then
  again ;

: enable-sprites
  [ LCDCF_ON
    LCDCF_BG8000 or
    LCDCF_BGOFF or
    LCDCF_OBJ8 or
    LCDCF_OBJON or ]L
  rLCDC c! ;

: slow
  500 begin
  dup 0 > while
    1 -
  repeat drop ;

: main
  init-input
  install-font
  disable-lcd
  _OAM [ 40 4 * ]L erase
  enable-sprites

  #1 player.tile lcd-wait-vblank c!
  %00000000 player.opts lcd-wait-vblank c!
  #80 player.y lcd-wait-vblank c!
  #50 player.x lcd-wait-vblank c!

  handle-input ;
