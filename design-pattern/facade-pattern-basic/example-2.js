// # Example 2
/* Complex parts */
class CPU {
  freeze() {
    /* code here */
  }
  jump(position) {
    /* code here */
  }
  execute() {
    /* code here */
  }
}

class Memory {
  load(position, data) {
    /* code here */
  }
}

class HardDrive {
  read(lba, size) {
    /* code here */
  }
}

/* Facade */
class ComputerFacade {
  constructor() {
    this.processor = new CPU();
    this.ram = new Memory();
    this.hd = new HardDrive();
  }

  start() {
    this.processor.freeze();
    this.ram.load(
      this.BOOT_ADDRESS,
      this.hd.read(this.BOOT_SECTOR, this.SECTOR_SIZE),
    );
    this.processor.jump(this.BOOT_ADDRESS);
    this.processor.execute();
  }
}

/* Usage */
let computer = new ComputerFacade();
computer.start();
