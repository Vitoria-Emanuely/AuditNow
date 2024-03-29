/* eslint-disable @typescript-eslint/adjacent-overload-signatures */

export class AlertService {
    danger(arg0: any) {
      throw new Error('Method not implemented.');
    }
 
    constructor() {}
  
    successIcon(message) {
      return {
        type: 'success',
        icon: 'mdi mdi-check-circle',
        message: message  
      };
    }
  
    errorIcon(message) {
      return {
        type: 'danger',
        icon: 'mdi mdi-block-helper',
        message: message  
      };
    }
  
    warningIcon(message) {
      return {
        type: 'warning',
        icon: 'mdi mdi-alert',
        message: message  
      };
    }
    
  }
  