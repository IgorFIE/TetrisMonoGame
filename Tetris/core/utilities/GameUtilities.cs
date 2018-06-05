namespace Tetris.core {
    class GameUtilities {

        public static int retrieveLeftValue(int currentValue, int amount) {
            currentValue -= amount; 
            if (currentValue < 0) {
                return 0;
            }
            return currentValue;
        }

        public static int retrieveRightValue(int currentValue, int amount) {
            currentValue += (amount + 1);
            if (currentValue >= GameProperties.BOARD_X_SIZE) {
                return GameProperties.BOARD_X_SIZE;
            }
            return currentValue;
        }

        public static int retrieveTopValue(int currentValue, int amount) {
            currentValue -= amount;
            if (currentValue < 0) {
                return 0;
            }
            return currentValue;
        }

        public static int retrieveBottomValue(int currentValue, int amount) {
            currentValue += (amount + 1);
            if (currentValue >= GameProperties.BOARD_Y_SIZE) {
                return GameProperties.BOARD_Y_SIZE;
            }
            return currentValue;
        }
    }
}
