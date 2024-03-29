﻿/*
Author: Jye Tremlett
Created: 7/7/2023
*/

window.onload = startFlow();

/*
Function to be called when when user chooses a width from the radio buttons and selects the submit button.
A new tab object is created and the tab segment input boxes of the appropriate width are dispalyed. Function 
then adds an event listener for the "copy-text-button" button once a user has entered the values for their tab.
*/
function startFlow() {

    // get width from radio buttons
    let width;
    if (document.getElementById('width8').checked) {
        width = document.getElementById('width8').value;
    }
    else if (document.getElementById('width16').checked) {
        width = document.getElementById('width16').value;
    }

    // create tab object, show user input interface, and show empty visualised tab segment
    let tab = new Tab(width);
    tab.displayInputBoxes('.tab-segment-value-input');

    // add onclick event listener for "copy to clipboard" button
    let btn = document.getElementById('copy-text-button');
    btn.addEventListener('click', function () {
        tab.getInput(); // update tab.data property before displaying tab
        tab.copyTabToClipboard(width);
    }, false);



    /* 
    Construction function for a Tab object. Tab object represents a guitar tab that is stored as a 2D 
    array. Width is the number of spaces for fret number inputs in each string (in other words, the
    number of forms in each row).
    */
    function Tab(width) {

        // data property is a 2D array holding tab values
        this.data = new Array(6);

        // set number of increments in a tab
        this.width = width;

        // build rows and columns
        for (let string = 0; string < 6; string++) {
            this.data[string] = new Array(width);
            for (let increment = 0; increment < width; increment++) {
                this.data[string][increment] = '';
            }
        }



        /*
        This function displays the interface for users to enter tab values into. The "section" field is a string
        of the class of the html element to display the interface within.
        */
        this.displayInputBoxes = function (section) {

            let context = document.querySelector(section);
            let output = '';
            let stringnames = ['e', 'B', 'G', 'D', 'A', 'E']

            // output += '<h2>Step 2: Fill in the Tab Segment as Required</h2>';
            for (let string = 0; string < 6; string++) {
                output += (stringnames[string] + '|');
                for (let increment = 0; increment < width; increment++) {
                    // set id to the xy coordinates of the field. This is used later to identify
                    // where the field's input should be stored within the data[][] array.
                    let id = `"${string}-${increment}"`;
                    output += `<input type="number" id=${id} class="note" placeholder="-">`;
                }
                output += '|<br>';
            }

            // set context's inner html to a string representation of the tab
            context.innerHTML = output;
        }



        /*
        function that parses each 'note' input field and updates the tab.data array with any new input.
        */
        this.getInput = function () {

            let string;
            let increment;
            let notes = document.getElementsByClassName('note'); // get all tab input field values

            // for each nonempty field, copy into this.data array
            for (const note of notes) {
                if (note.value != "") {
                    // note.id contains the xy coordinates needed to correctly place note value in data[][]  
                    string = parseInt(note.id.split('-')[0]);
                    increment = parseInt(note.id.split('-')[1]);

                    this.data[string][increment] = note.value;
                }
            }
        }



        /*
        Function to be called when a user selects the "copy text" button. Function converts this.data to string format
        and copies it to the user's clipboard.
        */
        this.copyTabToClipboard = function () {

            let note;
            let output = '';

            let stringnames = ['e', 'B', 'G', 'D', 'A', 'E']

            for (let string = 0; string < 6; string++) {
                output += (stringnames[string] + '|');
                for (let increment = 0; increment < width; increment++) {

                    // get current note
                    note = this.data[string][increment];

                    if (note == '') note = '--';

                    // append a dash if the fret number of the note is only one digit. 
                    // this is  important for spacing the output.
                    else if (parseInt(note) < 10) note += '-';

                    // add an extra dash so that adjacent two digit fret numbers don't clash
                    output += (note + '-');
                }
                output += '|\n';
            }

            // copy the text inside the text field
            navigator.clipboard.writeText(output);
        }

    }

}