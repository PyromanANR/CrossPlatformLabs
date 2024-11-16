import React, { useEffect, useState } from 'react';
import axios from 'axios';

axios.defaults.headers.common['Accept-Charset'] = 'UTF-8';
axios.defaults.headers.common['Content-Type'] = 'application/json; charset=utf-8';

const LabPage = ({ labNumber }) => {
    const [labData, setLabData] = useState(null);
    const [inputContent, setInputContent] = useState('');
    const [outputContent, setOutputContent] = useState('');
    const [error, setError] = useState(null);
    var LabNumber = parseInt(labNumber, 10);

    useEffect(() => {
        const fetchLabData = async () => {
            try {
                const response = await axios.get(`/api/Lab/lab${labNumber}`, {
                    headers: {
                        'Accept-Charset': 'UTF-8',
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                });
                setLabData(response.data);
                setError(null);
            } catch (error) {
                console.error('Error fetching lab data', error);
                setError('Failed to load lab data. Please try again later.');
            }
        };

        fetchLabData();
    }, [labNumber]);

    const handleFileChange = (e) => {
        const file = e.target.files?.[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (event) {
                setInputContent(event.target?.result);
            };
            reader.onerror = function () {
                setError('Error reading file');
            };
            reader.readAsText(file);
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError(null);

        const fileInput = document.getElementById('inputFile');
        if (!fileInput.files?.[0]) {
            setError('Please select a file');
            return;
        }

        const formData = new FormData();
        const inputFile = e.target.elements.inputFile.files[0];
        formData.append('labNumber', LabNumber);
        formData.append('inputFile', inputFile);

        try {
            const response = await axios.post('/api/Lab/processLab', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                },
            });
            setOutputContent(response.data.output);
        } catch (error) {
            console.error('Error submitting file', error);
            setError('Failed to process file. Please try again.');
        }
    };

    if (error) {
        return (
            <div className="container mt-4">
                <div className="alert alert-danger" role="alert">
                    {error}
                </div>
            </div>
        );
    }

    if (!labData) {
        return (
            <div className="container mt-4 d-flex justify-content-center">
                <div className="spinner-border text-primary" role="status">
                    <span className="visually-hidden">Loading...</span>
                </div>
            </div>
        );
    }

    return (
        <div className="container mt-4">
            <div className="card">
                <div className="card-header">
                    <h2>Laboratory Work #{labData.taskNumber}</h2>
                    <h4>Variant {labData.taskVariant}</h4>
                </div>

                <div className="card-body">
                    <div className="mb-4">
                        <h5>Task:</h5>
                        <p>{labData.taskDescription}</p>

                        <h5>Input Data:</h5>
                        <p>{labData.inputDescription}</p>

                        <h5>Output Data:</h5>
                        <p>{labData.outputDescription}</p>
                    </div>

                    {labData.testCases && labData.testCases.length > 0 && (
                        <div className="mb-4">
                            <h5>Examples:</h5>
                            <table className="table table-bordered">
                                <thead>
                                    <tr>
                                        <th>INPUT.TXT</th>
                                        <th>OUTPUT.TXT</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {labData.testCases.map((test, index) => (
                                        <tr key={index}>
                                            <td>
                                                <pre className="mb-0">{test.input}</pre>
                                            </td>
                                            <td>
                                                <pre className="mb-0">{test.output}</pre>
                                            </td>
                                        </tr>
                                    ))}
                                </tbody>
                            </table>
                        </div>
                    )}

                    <div className="card">
                        <div className="card-body">
                            <h5>Solution Check:</h5>
                            <form onSubmit={handleSubmit} encType="multipart/form-data">
                                <div className="mb-3">
                                    <label htmlFor="inputFile" className="form-label">
                                        Input File:
                                    </label>
                                    <input
                                        type="file"
                                        className="form-control"
                                        id="inputFile"
                                        name="inputFile"
                                        required
                                        onChange={handleFileChange}
                                        accept=".txt"
                                    />
                                </div>

                                <div className="mb-3">
                                    <label className="form-label">Content of Input File:</label>
                                    <textarea
                                        className="form-control"
                                        id="inputContent"
                                        rows="4"
                                        value={inputContent}
                                        readOnly
                                    ></textarea>
                                </div>

                                <div className="mb-3">
                                    <label className="form-label">Result:</label>
                                    <textarea
                                        className="form-control"
                                        id="outputContent"
                                        rows="4"
                                        value={outputContent}
                                        readOnly
                                    ></textarea>
                                </div>

                                <button type="submit" className="btn btn-primary">
                                    Check
                                </button>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    );
};

export default LabPage;
