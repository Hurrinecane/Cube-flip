namespace Cube_flip
{
    class FlipCubeGame
    {
        private int currentPanelX;
        private int currentPanelY;
        private int startPanelX;
        private int startPanelY;
        private int finishPanelX;
        private int finishPanelY;
        private desiredColorSide currentDesiredColorSide;

        public enum desiredColorSide
        {
            left,
            bottom,
            top,
            right,
            front,
            behind
        }

        public enum turningSide
        {
            left,
            bottom,
            top,
            right
        }

        public FlipCubeGame(int startPanelX, int startPanelY, int finishPanelX, int finishPanelY, desiredColorSide currentDesiredColorSide)
        {
            this.startPanelX = startPanelX;
            this.startPanelY = startPanelY;
            this.finishPanelX = finishPanelX;
            this.finishPanelY = finishPanelY;
            this.currentDesiredColorSide = currentDesiredColorSide;
            currentPanelX = startPanelX;
            currentPanelY = startPanelY;
        }

        public int СurrentPanelX
        {
            get
            {
                return currentPanelX;
            }
            set
            {
                currentPanelX = value;
            }
        }

        public int СurrentPanelY
        {
            get
            {
                return currentPanelY;
            }
            set
            {
                currentPanelY = value;
            }
        }

        public int StartPanelX
        {
            get
            {
                return startPanelX;
            }
            set
            {
                startPanelX = value;
            }
        }

        public int StartPanelY
        {
            get
            {
                return startPanelY;
            }
            set
            {
                startPanelY = value;
            }
        }

        public int FinishPanelX
        {
            get
            {
                return finishPanelX;
            }
            set
            {
                finishPanelX = value;
            }
        }

        public int FinishPanelY
        {
            get
            {
                return finishPanelY;
            }
            set
            {
                finishPanelY = value;
            }
        }

        public desiredColorSide CurrentDesiredColorSide
        {
            get
            {
                return currentDesiredColorSide;
            }
            set
            {
                currentDesiredColorSide = value;
            }
        }

        public void changeCurrentColor(turningSide receivedTurningSide)
        {
            switch (receivedTurningSide)
            {
                case turningSide.left:
                    switch (currentDesiredColorSide)
                    {
                        case desiredColorSide.top:
                            currentDesiredColorSide = desiredColorSide.left;
                            break;
                        case desiredColorSide.left:
                            currentDesiredColorSide = desiredColorSide.bottom;
                            break;
                        case desiredColorSide.right:
                            currentDesiredColorSide = desiredColorSide.top;
                            break;
                        case desiredColorSide.bottom:
                            currentDesiredColorSide = desiredColorSide.right;
                            break;
                        case desiredColorSide.front:
                            currentDesiredColorSide = desiredColorSide.front;
                            break;
                        case desiredColorSide.behind:
                            currentDesiredColorSide = desiredColorSide.behind;
                            break;
                    }
                    break;
                case turningSide.right:
                    switch (currentDesiredColorSide)
                    {
                        case desiredColorSide.top:
                            currentDesiredColorSide = desiredColorSide.right;
                            break;
                        case desiredColorSide.left:
                            currentDesiredColorSide = desiredColorSide.top;
                            break;
                        case desiredColorSide.right:
                            currentDesiredColorSide = desiredColorSide.bottom;
                            break;
                        case desiredColorSide.bottom:
                            currentDesiredColorSide = desiredColorSide.left;
                            break;
                        case desiredColorSide.front:
                            currentDesiredColorSide = desiredColorSide.front;
                            break;
                        case desiredColorSide.behind:
                            currentDesiredColorSide = desiredColorSide.behind;
                            break;
                    }
                    break;
                case turningSide.top:
                    switch (currentDesiredColorSide)
                    {
                        case desiredColorSide.top:
                            currentDesiredColorSide = desiredColorSide.front;
                            break;
                        case desiredColorSide.left:
                            currentDesiredColorSide = desiredColorSide.left;
                            break;
                        case desiredColorSide.right:
                            currentDesiredColorSide = desiredColorSide.right;
                            break;
                        case desiredColorSide.bottom:
                            currentDesiredColorSide = desiredColorSide.behind;
                            break;
                        case desiredColorSide.front:
                            currentDesiredColorSide = desiredColorSide.bottom;
                            break;
                        case desiredColorSide.behind:
                            currentDesiredColorSide = desiredColorSide.top;
                            break;
                    }
                    break;
                case turningSide.bottom:
                    switch (currentDesiredColorSide)
                    {
                        case desiredColorSide.top:
                            currentDesiredColorSide = desiredColorSide.behind;
                            break;
                        case desiredColorSide.left:
                            currentDesiredColorSide = desiredColorSide.left;
                            break;
                        case desiredColorSide.right:
                            currentDesiredColorSide = desiredColorSide.right;
                            break;
                        case desiredColorSide.bottom:
                            currentDesiredColorSide = desiredColorSide.front;
                            break;
                        case desiredColorSide.front:
                            currentDesiredColorSide = desiredColorSide.top;
                            break;
                        case desiredColorSide.behind:
                            currentDesiredColorSide = desiredColorSide.bottom;
                            break;
                    }
                    break;
            }
        }
    }
}